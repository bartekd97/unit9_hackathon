using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour {

    public Transform frontPoint;
    public float movementSpeed = 2f;
    public float maxDistance = 2f;
    public float maxFrontSideDistance = 2f;

    private const float C_RAYCAST_REFRESH_TIME = 0.25f;

    private bool forceWait = false;

    private Vector3 toPosition;
    private Vector3 movementDirection;
    private float _time;
    void Start () {
        if (GameGlobal.isGameStarted)
            CalculateMovementDirection();
        _time = Time.time;
    }
	

	void Update () {
        if (!GameGlobal.isGameStarted)
            return;
        float distance = (frontPoint.position - toPosition).magnitude;
        if (distance < maxDistance)
            return;

        if (Time.time - _time >= C_RAYCAST_REFRESH_TIME)
        {
            _time = Time.time;
            CalculateMovementDirection();
        }

        Debug.DrawLine(frontPoint.position, frontPoint.position + frontPoint.forward * maxFrontSideDistance - frontPoint.right * maxFrontSideDistance);
        Debug.DrawLine(frontPoint.position, frontPoint.position + frontPoint.forward * maxFrontSideDistance + frontPoint.right * maxFrontSideDistance);

        if (forceWait)
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
            return;
        }

        
        Vector3 position = new Vector3(transform.position.x, 0f, transform.position.z);
        position += movementDirection * Time.deltaTime * movementSpeed;
        float atg = Mathf.Atan2(toPosition.x - position.x, toPosition.z - position.z);
        Quaternion rotation = Quaternion.Euler(0, atg*Mathf.Rad2Deg, 0);

        transform.rotation = rotation;
        transform.position = position;

        Debug.DrawLine(position + Vector3.up, toPosition + Vector3.up);
        

        //transform.LookAt(toPosition);
        //transform.Translate(movementDirection * Time.deltaTime * movementSpeed);
    }

    private void CalculateMovementDirection()
    {
        toPosition = GameGlobal.bitcoinMiner.transform.position;
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
        }

        // sprawdzamy czy po bokach czegos nie ma
        forceWait = false;
        wasHit = Physics.Raycast(frontPoint.position, frontPoint.forward - frontPoint.right, out hit); // strona lewa
        if (wasHit)
        {
            if (hit.distance <= maxFrontSideDistance && !hit.transform.gameObject.CompareTag("BitcoinMiner"))
            {
                forceWait = true;
                return;
            }
        }
        wasHit = Physics.Raycast(frontPoint.position, frontPoint.forward + frontPoint.right, out hit); // strona prawa
        if (wasHit)
        {
            if (hit.distance <= maxFrontSideDistance && !hit.transform.gameObject.CompareTag("BitcoinMiner"))
            {
                forceWait = true;
                return;
            }
        }
    }
}
