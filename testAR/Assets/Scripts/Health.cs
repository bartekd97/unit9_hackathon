using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    public bool baseObject;
	void Start () {
        currentHealth = maxHealth;
	}
	
    public void SubtractHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    private void Die()
    {
        if (baseObject)
        {
            //Show losing screen, reset game or something
        }
        else Destroy(gameObject);
    }
}
