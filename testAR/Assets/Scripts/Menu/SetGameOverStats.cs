using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameOverStats : MonoBehaviour {

    public UnityEngine.UI.Text text;
	void Start () {
        text.text = "BTC: " + GameGlobal.bitcoinsCurrency.ToString() + "\nLevel: " + GameGlobal.currentLevel.ToString() + "\nXP: " + GameGlobal.totalExperience.ToString();
	}
	
}
