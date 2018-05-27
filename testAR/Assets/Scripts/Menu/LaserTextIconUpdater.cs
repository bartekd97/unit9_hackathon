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

        LevelDamage.text = "Level: " + gameObject.transform.parent.gameObject.GetComponent<SentryGunController>().level.ToString();
        LevelDamage.text += "\nDamage: " + Mathf.Round(gameObject.transform.parent.gameObject.GetComponent<SentryGunController>().laserDamage).ToString();
        UpgradePrice.text = "Upgrade: $" + (GameObject.Find("Shop").GetComponent<Shop>().prices["SentryLevelUp"]).ToString();
        SellValue.text = "Value: $ " + (Mathf.RoundToInt(GameObject.Find("Shop").GetComponent<Shop>().prices["SentryLevelUp"]/3)).ToString();
        UpgradeIcon.SetIconType(MenuIconContainer.IconType.Active);

        /*
        ShotLeftTime.text = "";
        if (gameObject.transform.parent.gameObject.GetComponent<SentryGunController>().timeoutLeft > 0)
        {
            ShootIcon.SetIconType(MenuIconContainer.IconType.Disabled);
            ShotLeftTime.text = Mathf.RoundToInt(gameObject.transform.parent.gameObject.GetComponent<SentryGunController>().timeoutLeft).ToString();
        }
        else
        {
            ShootIcon.SetIconType(MenuIconContainer.IconType.Active);
            ShotLeftTime.text = "";
        }
        */
    }
}
