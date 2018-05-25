using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinCurrencyDisplay : MonoBehaviour {
    private UnityEngine.UI.Text canvasText;
	void Start () {
        canvasText = GetComponent<UnityEngine.UI.Text>();
    }
	
	void Update () {
        if (GameGlobal.isGameStarted)
            canvasText.text = "Zebrane bitcoiny: " + GameGlobal.bitcoinsCurrency.ToString();
        else
            canvasText.text = "";
    }
}
