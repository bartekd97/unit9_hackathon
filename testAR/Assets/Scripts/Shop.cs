using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {


    public Dictionary<string, float> prices = new Dictionary<string, float>();
	void Start () {
        prices.Add("dupa", 50000);
        prices.Add("Laser", 50000);
        prices.Add("DoubleLaser", 50000);
        prices.Add("WideLaser", 50000);
        prices.Add("BombShooter", 50000);

        prices.Add("PUFire", 2);
        prices.Add("PUSlowDown", 1);
        prices.Add("PUFreeze", 1);
        prices.Add("PUWeaken", 2);

        prices.Add("LevelUp", 3500);
	}
	
    public void Buy(string product)
    {
        
        if (product.Substring(0, 2) == "PU")
        {
            GameGlobal.playerTokens -= (int)prices[product];
            foreach(GameObject p in GameObject.FindGameObjectsWithTag("Sentry"))
            {
                string powerUp = product.Substring(2);
                p.GetComponent<SentryGunController>().GetPowerUp(powerUp);
            }
        }
        else GameGlobal.SubtractCash("usd", prices[product]);


        //do stuff with switch case here

    }
	void Update () {
		
	}
}
