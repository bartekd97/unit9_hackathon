using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayController : MonoBehaviour {

    [HideInInspector]
    public float damage;

    private bool enemyInRay;
    private SentryGunController sentryController;

    private void Start()
    {
        enemyInRay = false;
        sentryController = gameObject.transform.parent.gameObject.GetComponent<SentryGunController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyInTrigger(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy") enemyInRay = false;
    }

    public void OnChildTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyInTrigger(other);
        }
    }

    void EnemyInTrigger(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (sentryController.powerUps["SlowDown"] && !enemy.affectedBy["SlowDown"]) enemy.SlowDownEffect(sentryController.powerUpsValues["SlowDown"]);
        if (sentryController.powerUps["Weaken"] && !enemy.affectedBy["Weaken"]) enemy.WeakenEffect(sentryController.powerUpsValues["Weaken"]);
        if (sentryController.powerUps["Fire"] && !enemy.affectedBy["Fire"]) enemy.FireEffect(sentryController.powerUpsValues["Fire"], 3f, 0.3f);
        if (sentryController.powerUps["Freeze"] && !enemy.affectedBy["Freeze"]) enemy.FreezeEffect(sentryController.powerUpsValues["Freeze"]);
        enemyInRay = true;
        StartCoroutine(DealingDamage(other.gameObject.GetComponentInParent<Health>()));
    }

    public void OnChildTriggerExit(Collider other)
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
