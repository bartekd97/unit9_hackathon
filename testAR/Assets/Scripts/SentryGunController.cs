using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGunController : MonoBehaviour {

    public float shotDuration;
    public float laserDamage;

    private GameObject laserRay;
	void Start () {
        laserRay = gameObject.transform.GetChild(0).gameObject;
        laserRay.GetComponent<LaserRayController>().damage = laserDamage;
        laserRay.SetActive(false);
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.X)) StartCoroutine(LaserShot(shotDuration));
	}

    IEnumerator LaserShot(float duration)
    {
        laserRay.SetActive(true);
        yield return new WaitForSeconds(duration);
        laserRay.SetActive(false);
    }
}
