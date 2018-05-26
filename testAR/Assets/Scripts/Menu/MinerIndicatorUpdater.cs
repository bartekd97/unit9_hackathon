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
        LevelXP.text = "Level: 3\nXP: 12345";
        Tokens.text = "Available tokens: 3";
        BTC.text = "Currency: 1,237 BTC\n1 BTC = 23,456$";
	}
}
