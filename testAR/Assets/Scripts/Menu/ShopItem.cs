using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public string name;
    public float price;
    public Text nameText;
    public Text priceText;
    public MenuIconContainer icon;

	void Update () {
        nameText.text = name;
        priceText.text = "$" + price.ToString();
        if (price > 10000)
            icon.SetIconType(MenuIconContainer.IconType.Disabled);
        else
            icon.SetIconType(MenuIconContainer.IconType.Active);
	}
}
