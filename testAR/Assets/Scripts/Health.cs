using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    public bool baseObject;
    public float deltaHealth;

    private LevelController levelController;
	void Start () {
        currentHealth = maxHealth;
        levelController = GameObject.Find("LevelHandler").GetComponent<LevelController>();
        StartCoroutine(CalculatingHealthDelta());
	}
	
    public void SubtractHealth(float amount)
    {
        //Blink b = GetComponent<Blink>();
        //if (b != null)
        //    b.ChangeBool();

        if (currentHealth <= 0)
            return;
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
        
        GameGlobal.CountEnemies();
        if (gameObject.tag == "Enemy") levelController.AddExperience(gameObject.GetComponent<Enemy>().expForKill);

        if (baseObject)
        {
            //Show losing screen, reset game or something
            gameObject.GetComponent<BitcoinMiner>().SetParticlesState(false);
            gameObject.GetComponent<BitcoinMiner>().GameOver();
            Time.timeScale = 0;
        }
        else Destroy(gameObject);

    }

    IEnumerator CalculatingHealthDelta()
    {
        while (true)
        {
            float temp = currentHealth;
            yield return new WaitForSeconds(.4f);
            deltaHealth = currentHealth - temp;
            if (baseObject)
                gameObject.GetComponent<BitcoinMiner>().SetParticlesState(deltaHealth < 0 ? true : false);
        }
    }
}
