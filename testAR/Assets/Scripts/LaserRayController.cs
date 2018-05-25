using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayController : MonoBehaviour {

    [HideInInspector]
    public float damage;

    private bool enemyInRay;

    private void Start()
    {
        enemyInRay = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemyInRay = true;
            StartCoroutine(DealingDamage(other.gameObject.GetComponent<Health>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy") enemyInRay = false;
    }

    IEnumerator DealingDamage(Health enemy)
    {
        while (enemyInRay)
        {
            enemy.SubtractHealth(damage);
            yield return new WaitForSeconds(0.1f);
        }

        if (!enemyInRay) yield break;

    }
}
