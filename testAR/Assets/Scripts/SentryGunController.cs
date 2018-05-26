using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGunController : MonoBehaviour {

    [Tooltip("Set this value negative to have laser turned on constantly")]
    public float shotDuration;
    public float laserDamage;
    public float timeout;
    public float timeoutLeft;
    public int level;

    public Dictionary<string, bool> powerUps = new Dictionary<string, bool>();
    public Dictionary<string, bool> powerUpsUpgraded = new Dictionary<string, bool>();
    public Dictionary<string, float> powerUpsValues = new Dictionary<string, float>();

    private bool canShoot;
    private GameObject laserRay;
	void Start () { 
        level = 1;
        canShoot = true;
        powerUps.Add("Freeze", false);
        powerUps.Add("SlowDown", false);
        powerUps.Add("Fire", false);
        powerUps.Add("Weaken", false);
        powerUps.Add("LiquoredUp", false);
        powerUps.Add("Suicide", false);
        powerUpsUpgraded.Add("Freeze", false);
        powerUpsUpgraded.Add("SlowDown", false);
        powerUpsUpgraded.Add("Fire", false);
        powerUpsUpgraded.Add("Weaken", false);
        powerUpsUpgraded.Add("LiquoredUp", false);
        powerUpsUpgraded.Add("Suicide", false);

        powerUpsValues["Freeze"] = 1.3f;
        powerUpsValues["Weaken"] = 1.5f;
        powerUpsValues["SlowDown"] = 1f;
        powerUpsValues["Fire"] = 1.25f;
        powerUpsValues.Add("LiquoredUp", 1.5f);
        powerUpsValues.Add("Suicide", 3f);

        laserRay = gameObject.transform.GetChild(0).gameObject;
        laserRay.GetComponent<LaserRayController>().damage = laserDamage;
        laserRay.SetActive(false);

        if (shotDuration < 0) laserRay.SetActive(true);
	}
	
	public void GetPowerUp(string powerUpName)
    {
        if (!powerUps[powerUpName])
        {
            powerUps[powerUpName] = true;
            Debug.Log("PowerUp Got! " + powerUpName);
        }
        else if (!powerUpsUpgraded[powerUpName])
        {
            powerUpsValues[powerUpName] *= 1.37f;
            powerUpsUpgraded[powerUpName] = true;
        }

        
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && shotDuration > 0 && canShoot) Shoot();
	}
    public void Shoot()
    {
        StartCoroutine(LaserShot(shotDuration));
    }
    public void UpgradeSelf()
    {
        level++;
        laserDamage += 3;
    }
    IEnumerator Timeout(float time)
    {
        canShoot = false;
        
        while(time > 0)
        {
            yield return new WaitForSeconds(0.2f);
            time -= 0.2f;
            timeoutLeft = time;
        }

        canShoot = true;
    }
    IEnumerator LaserShot(float duration)
    {
        laserRay.SetActive(true);
        yield return new WaitForSeconds(duration);
        laserRay.SetActive(false);
        StartCoroutine(Timeout(timeout));
    }
}
