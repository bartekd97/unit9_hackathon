using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {

    private float damage;
    private float duration;

    private EnemyGun enemyGunController;
	void Start () {
        enemyGunController = gameObject.transform.parent.gameObject.GetComponent<EnemyGun>();
        damage = enemyGunController.laserDamage;
        duration = enemyGunController.shotDuration;
	}


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other in trigger: " + other.gameObject.tag);
        if (other.gameObject.tag == "BitcoinMinerCollision")
        {
            other.gameObject.GetComponentInParent<Health>().SubtractHealth(damage);
        }
    }

}
