using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGunController : MonoBehaviour {

    [Tooltip("Set this value negative to have laser turned on constantly")]
    public float shotDuration;
    public float laserDamage;

    public Dictionary<string, bool> powerUps = new Dictionary<string, bool>();
    public Dictionary<string, bool> powerUpsUpgraded = new Dictionary<string, bool>();
    public Dictionary<string, float> powerUpsValues = new Dictionary<string, float>();

    private GameObject laserRay;
	void Start () {
        powerUps.Add("Freeze", false);
        powerUps.Add("SlowDown", false);
        powerUps.Add("Fire", false);
        powerUps.Add("Weaken", false);
        powerUpsUpgraded.Add("Freeze", false);
        powerUpsUpgraded.Add("SlowDown", false);
        powerUpsUpgraded.Add("Fire", false);
        powerUpsUpgraded.Add("Weaken", false);

        powerUpsValues["Freeze"] = 1.3f;
        powerUpsValues["Weaken"] = 1.5f;
        powerUpsValues["SlowDown"] = 1f;
        powerUpsValues["Fire"] = 1.25f;

        laserRay = gameObject.transform.GetChild(0).gameObject;
        laserRay.GetComponent<LaserRayController>().damage = laserDamage;
        laserRay.SetActive(false);

        if (shotDuration < 0) laserRay.SetActive(true);
	}
	
	public void GetPowerUp(string powerUpName)
    {
        if (!powerUps[powerUpName]) powerUps[powerUpName] = true;
        else if (!powerUpsUpgraded[powerUpName])
        {
            powerUpsValues[powerUpName] *= 1.37f;
            powerUpsUpgraded[powerUpName] = true;
        }
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && shotDuration > 0) Shoot();
	}
    public void Shoot()
    {
        StartCoroutine(LaserShot(shotDuration));
    }
    IEnumerator LaserShot(float duration)
    {
        laserRay.SetActive(true);
        yield return new WaitForSeconds(duration);
        laserRay.SetActive(false);
    }
}
