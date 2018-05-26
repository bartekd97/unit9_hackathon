using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {

    private GameObject sentryParent;
    private Shop shop;
    private void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        sentryParent = gameObject.transform.parent.parent.parent.gameObject;
    }
    public void TriggerShot()
    {
        sentryParent.GetComponent<SentryGunController>().Shoot();
    }

    public void UpgradeSentry()
    {
        if (GameGlobal.CanBuy(shop.prices["SentryLevelUp"]))
        {
            GameGlobal.SubtractCash("usd", shop.prices["SentryLevelUp"]);
            sentryParent.GetComponent<SentryGunController>().UpgradeSelf();
            shop.prices["SentryLevelUp"] *= 1.4f;
        }
    }
    public void Sell()
    {
        GameGlobal.AddCash("usd", Mathf.RoundToInt(shop.prices["SentryLevelUp"] / 3));
        Destroy(sentryParent, 0.2f);
    }

    //public void BuyPowerUp()
    //{
    //    string powerup = name;
    //    if(GameGlobal.playerTokens >= gameObject.GetComponent<PowerupItem>().tokens)
    //    {
    //        sentryParent.GetComponent<SentryGunController>().GetPowerUp(powerup);
    //    }
    //}
}
