using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {
    public bool isFlatSentry;

    public GameObject sentryParent;
    private Shop shop;
    private void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
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
        if (isFlatSentry)
        {
            Destroy(sentryParent.transform.parent.gameObject, 0.2f);
        }
        else
        {
            Destroy(sentryParent, 0.2f);
        }
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
