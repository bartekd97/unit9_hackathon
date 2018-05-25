using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitcoinDisplay : MonoBehaviour {

    private Text display;
	void Start () {
        display = GameObject.Find("BitcoinDisplay").GetComponent<Text>();
	}
	
	
	void Update () {
        display.text = "BTC: " + GameGlobal.bitcoinsCurrency.ToString() + "\n$: " + (GameGlobal.bitcoinsCurrency * GameGlobal.bitcointExchange).ToString();
	}
}
