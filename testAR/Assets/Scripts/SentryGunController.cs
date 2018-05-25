using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGunController : MonoBehaviour {

    [Tooltip("Set this value negative to have laser turned on constantly")]
    public float shotDuration;
    public float laserDamage;

    private GameObject laserRay;
	void Start () {
        laserRay = gameObject.transform.GetChild(0).gameObject;
        laserRay.GetComponent<LaserRayController>().damage = laserDamage;
        laserRay.SetActive(false);

        if (shotDuration < 0) laserRay.SetActive(true);
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && shotDuration > 0) StartCoroutine(LaserShot(shotDuration));
	}

    IEnumerator LaserShot(float duration)
    {
        laserRay.SetActive(true);
        yield return new WaitForSeconds(duration);
        laserRay.SetActive(false);
    }
}
