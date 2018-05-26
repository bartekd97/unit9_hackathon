using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {


    public Dictionary<string, float> prices = new Dictionary<string, float>();
	void Start () {
        prices.Add("dupa", 50000);
	}
	
    public void Buy(string product)
    {
        GameGlobal.SubtractCash("usd", prices[product]);
        //do stuff with switch case here
    }
	void Update () {
		
	}
}
