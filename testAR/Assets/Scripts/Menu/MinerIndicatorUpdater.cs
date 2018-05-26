using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerIndicatorUpdater : MonoBehaviour {

    public Text LevelXP;
    public Text Tokens;
    public Text BTC;

    void Update ()
    {
        LevelXP.text = "Level: " + GameGlobal.currentLevel.ToString() + "\nXP: " + GameGlobal.totalExperience.ToString();
        Tokens.text = "Available tokens: " + GameGlobal.playerTokens.ToString();
        BTC.text = "Currency: " + GameGlobal.bitcoinsCurrency.ToString() + " BTC\n1 BTC = " + GameGlobal.bitcoinExchange.ToString() + "$";
	}
}
