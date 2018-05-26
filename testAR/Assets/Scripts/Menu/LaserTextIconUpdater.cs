using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTextIconUpdater : MonoBehaviour {

    public Text LevelDamage;
    public Text UpgradePrice;
    public Text SellValue;
    public MenuIconContainer UpgradeIcon;

    public Text ShotLeftTime;
    public MenuIconContainer ShootIcon;

    void Update()
    {
        //if (!GameGlobal.isGameStarted)
        //    return;

        LevelDamage.text = "Level: 1\nDamage: 21.37";
        UpgradePrice.text = "Upgrade: $50000";
        SellValue.text = "Value: $7777";
        UpgradeIcon.SetIconType(MenuIconContainer.IconType.Disabled);

        ShotLeftTime.text = "21.37s";
        ShootIcon.SetIconType(MenuIconContainer.IconType.Disabled);
    }
}
