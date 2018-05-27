using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public float shotDuration;
    public float laserDamage;
    public float interval;

    private GameObject laserRay;
    void Start () {
        laserRay = gameObject.transform.GetChild(0).gameObject;
        laserRay.SetActive(false);
        StartCoroutine(Shooting());
    }
	
    IEnumerator Shooting()
    {
        while (true)
        {
            laserRay.SetActive(true);
            yield return new WaitForSeconds(shotDuration);
            laserRay.SetActive(false);
            yield return new WaitForSeconds(interval);
        }
    }
}
