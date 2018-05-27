using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour {

    public Transform frontPoint;
    public float movementSpeed = 2f; // szybkosc poruszania sie
    public float maxDistance = 2f; // minimalny dystans przed frontem (sluzy tez do wykrywania czy dotarl do koparki)
    public float maxDistanceForNewPath = 4f; // maksymalny dystans przed frontem jesli w poblizu jest przeszkoda w celu wyznaczenia nowej sciezki
    public float maxFrontSideDistance = 2f; // minimalny dystans z frontu na skos
    public float forceWaitTimeToNewPath = 2f; // czas jaki uplynie zanim nastapi proba wyznaczenia nowej sciezki jesli robot sie zablokuje
    public float minimumDistanceForNewPath = 4f; // minimalny dystans do przebycia na nowej sciezce (w praktyce jest pomniejszony o maxDistance)

    public float damagePerTick;
    public float tickTime;

    private GameObject bitcoinMiner;
    private const float C_RAYCAST_REFRESH_TIME = 0.25f; // 4 razy na sekunde

    public bool reachedDestination = false;
    public bool _reachedMiner = false;
    public bool reachedMiner { get {  return _reachedMiner; } }
    public bool forceWait = false;
    public bool keepPathToAction = false;


    private Vector3 toPosition;
    private Vector3 movementDirection;
    private float _time;
    private float forceWaitSince;
    private float posY;
    void Start () {
        bitcoinMiner = GameObject.FindGameObjectWithTag("BitcoinMiner");
        posY = transform.position.y;
        RotateToPoint(bitcoinMiner.transform.position);
        //if (GameGlobal.isGameStarted)
            CalculateMovementDirection();
        _time = Time.time;
        forceWaitSince = Time.time;
    }
	

	void Update () {
        //if (!GameGlobal.isGameStarted || _reachedMiner)
        if ( _reachedMiner)
            return;
        float distance = (frontPoint.position - toPosition).magnitude;
        if (distance < maxDistance)
        {
            reachedDestination = true;
            if (keepPathToAction)
                CalculateMovementDirection();
            else
            {
                _reachedMiner = true;
                StartCoroutine(DamageDealing());
                return;
            }
        }
        reachedDestination = false;

        if (Time.time - _time >= C_RAYCAST_REFRESH_TIME)
        {
            _time = Time.time;
            CalculateMovementDirection();
        }

        //Debug.DrawLine(frontPoint.position, frontPoint.position + frontPoint.forward * maxFrontSideDistance - frontPoint.right * maxFrontSideDistance);
        //Debug.DrawLine(frontPoint.position, frontPoint.position + frontPoint.forward * maxFrontSideDistance + frontPoint.right * maxFrontSideDistance);

        if (forceWait)
        {
            //Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
            return;
        }

        
        Vector3 position = new Vector3(transform.position.x, posY, transform.position.z);
        position += movementDirection * Time.deltaTime * movementSpeed;
        transform.position = position;
        RotateToPoint(toPosition);

        /*
        if (keepPathToAction)
            Debug.DrawLine(position + Vector3.up, toPosition + Vector3.up, Color.green);
        else
            Debug.DrawLine(position + Vector3.up, toPosition + Vector3.up);

        for (float forward = 0.5f; forward >= -0.1f; forward -= 0.1f)
        {
            for (float side = 0.25f; side <= 2f; side += 0.25f)
            {
                position = frontPoint.position + (frontPoint.forward * forward + frontPoint.right * side).normalized * 4f;
                Debug.DrawLine(frontPoint.position, position, Color.yellow);
                position = frontPoint.position + (frontPoint.forward * forward - frontPoint.right * side).normalized * 4f;
                Debug.DrawLine(frontPoint.position, position, Color.yellow);
            }
        }
        //*/
    }

    private void RotateToPoint( Vector3 point)
    {
        float atg = Mathf.Atan2(point.x - transform.position.x, point.z - transform.position.z);
        transform.rotation = Quaternion.Euler(0, atg * Mathf.Rad2Deg, 0);
    }

    private void SetForceWait(bool wait)
    {
        if (wait)
        {
            if (!forceWait)
            {
                forceWait = true;
                forceWaitSince = Time.time;
            }
        }
        else
        {
            forceWait = false;
        }
    }

    private void CalculateMovementDirection()
    {
        if (!keepPathToAction || (keepPathToAction && (forceWait || reachedDestination)))
        {
            //toPosition = GameGlobal.bitcoinMiner.transform.position;
            toPosition = bitcoinMiner.transform.position;
            if (keepPathToAction)
            {
                RotateToPoint(toPosition);
                keepPathToAction = false;
            }
        }
        movementDirection = (toPosition - frontPoint.position).normalized;
        RaycastHit hit; bool wasHit;
        wasHit = Physics.Raycast(frontPoint.position, movementDirection, out hit);
        if (wasHit)
        {
            if (hit.transform.gameObject.CompareTag("BitcoinMiner"))
            {
                toPosition = hit.point;
                movementDirection = (toPosition - frontPoint.position).normalized;
            }
            else if (hit.distance < maxDistance)
            {
                TryToCalculateNewPath();
                return;
            }
            else if (hit.distance < maxDistanceForNewPath && Time.time - forceWaitSince >= C_RAYCAST_REFRESH_TIME*2 )
            {
                if (TryToCalculateNewPath())
                    return;
                else
                    forceWaitSince = Time.time;
            }
        }

        if (forceWait && Time.time - forceWaitSince >= forceWaitTimeToNewPath)
        {
            if (TryToCalculateNewPath())
            {
                SetForceWait(false);
                return;
            }
            else
                forceWaitSince = Time.time; // fail and wait again
        }

        // sprawdzamy czy po bokach czegos nie ma
        wasHit = Physics.Raycast(frontPoint.position, frontPoint.forward - frontPoint.right, out hit); // strona lewa
        if (wasHit)
        {
            if (hit.distance <= maxFrontSideDistance && !hit.transform.gameObject.CompareTag("BitcoinMiner"))
            {
                SetForceWait(true);
                return;
            }
        }
        wasHit = Physics.Raycast(frontPoint.position, frontPoint.forward + frontPoint.right, out hit); // strona prawa
        if (wasHit)
        {
            if (hit.distance <= maxFrontSideDistance && !hit.transform.gameObject.CompareTag("BitcoinMiner"))
            {
                SetForceWait(true);
                return;
            }
        }
        SetForceWait(false);
    }
    private bool TryToCalculateNewPath()
    {
        float dist = (frontPoint.position - bitcoinMiner.transform.position).magnitude;
        bool wasHit;
        Vector3 position;
        float mag;
        for (float forward = 0.5f; forward >= 0f; forward -= 0.1f)
        {
            for (float side = 0.25f; side <= 1f; side += 0.25f)
            {
                mag = (frontPoint.forward * dist * forward + frontPoint.right * dist * side).magnitude;
                position = frontPoint.position + frontPoint.forward * dist * forward + frontPoint.right * dist * side;
                wasHit = Physics.Raycast(frontPoint.position, position - frontPoint.position, minimumDistanceForNewPath);
                if (wasHit)
                {
                    position = frontPoint.position + frontPoint.forward * dist * forward - frontPoint.right * dist * side;
                    wasHit = Physics.Raycast(frontPoint.position, position - frontPoint.position, minimumDistanceForNewPath);
                }
                if (!wasHit)
                {
                    if (mag < minimumDistanceForNewPath)
                        toPosition = (position - frontPoint.position).normalized * minimumDistanceForNewPath;
                    else
                        toPosition = position;
                    movementDirection = (toPosition - frontPoint.position).normalized;
                    keepPathToAction = true;
                    return true;
                }
            }
        }
        return false;
    }
    IEnumerator DamageDealing()
    {
        while (_reachedMiner)
        {
            GameGlobal.bitcoinMiner.gameObject.GetComponent<Health>().SubtractHealth(damagePerTick);
            yield return new WaitForSeconds(tickTime);
        }
    }
}
