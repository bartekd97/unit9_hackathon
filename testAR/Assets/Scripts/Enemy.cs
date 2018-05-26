using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Dictionary<string, bool> affectedBy = new Dictionary<string, bool>();
    public float damage;
    public float movementSpeed;
    public int expForKill;

    private float movementSpeedHolder;
    private bool onFire;
	void Start () {
        affectedBy.Add("Freeze", false);
        affectedBy.Add("SlowDown", false);
        affectedBy.Add("Fire", false);
        affectedBy.Add("Weaken", false);

        onFire = false;
        gameObject.GetComponent<RobotAI>().movementSpeed = movementSpeed;
        gameObject.GetComponent<RobotAI>().damagePerTick = damage;

	}
	
    public void SlowDownEffect(float slowAmount)
    {
        if (!affectedBy["SlowDown"])
        {
            movementSpeed -= slowAmount;
            gameObject.GetComponent<RobotAI>().movementSpeed = movementSpeed;
            affectedBy["SlowDown"] = true;
        }
        
    }
    public void WeakenEffect(float weakAmount)
    {
        if (!affectedBy["Weaken"])
        {
            damage -= weakAmount;
            gameObject.GetComponent<RobotAI>().damagePerTick = damage;
            affectedBy["Weaken"] = true;
        }
    }

    public void FreezeEffect(float freezeTime)
    {
        StartCoroutine(FreezeEffectNumerator(freezeTime));
    }

    public void FireEffect(float fireTime, float damagePerTick, float tickTime)
    {
        StartCoroutine(FireEffectNumerator(fireTime, damagePerTick, tickTime));
    }

    IEnumerator FreezeEffectNumerator(float freezeTime)
    {
        Debug.Log("Being freezed! " + freezeTime);
        if (!affectedBy["Freeze"])
        {
            affectedBy["Freeze"] = true;
        }
        else
        {
            yield break;
        }
        movementSpeedHolder = movementSpeed;
        movementSpeed = 0;
        gameObject.GetComponent<RobotAI>().movementSpeed = movementSpeed;
        yield return new WaitForSeconds(freezeTime);
        Debug.Log("Being unfreezed!");
        movementSpeed = movementSpeedHolder;
        affectedBy["Freeze"] = false;
        gameObject.GetComponent<RobotAI>().movementSpeed = movementSpeed;
    }

    IEnumerator FireEffectNumerator(float fireTime, float damagePerTick, float tickTime)
    {
        if (!affectedBy["Fire"])
        {
            affectedBy["Fire"] = true;
        }
        else
        {
            yield break;
        }

        onFire = true;
        StartCoroutine(FireTimer(fireTime));
        while (onFire)
        {
            gameObject.GetComponent<Health>().SubtractHealth(damagePerTick);
            yield return new WaitForSeconds(tickTime);
        }
        affectedBy["Fire"] = false;
    }
    IEnumerator FireTimer(float time)
    {
        yield return new WaitForSeconds(time);
        onFire = !onFire;
    }

}
