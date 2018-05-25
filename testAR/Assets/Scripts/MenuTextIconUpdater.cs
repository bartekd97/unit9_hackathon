using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTextIconUpdater : MonoBehaviour {

    public Text LevelAndMake;
    public Text ExpToNext;
    public MenuIconContainer LevelupIcon;

	void Update () {
        if (!GameGlobal.isGameStarted)
            return;

        LevelAndMake.text = "Level: 3\nMake: 3 BTC/min";
        ExpToNext.text = "Exp to next lvl: 3000";
        LevelupIcon.SetIconType(MenuIconContainer.IconType.Disabled);
	}
}
