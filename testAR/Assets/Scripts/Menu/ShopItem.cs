﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public string name;
    public float price;
    public Text nameText;
    public Text priceText;
    public MenuIconContainer icon;
    public GameObject product;
    public GoogleARCore.Examples.HelloAR.HelloARController helloARController;

    private void Start()
    {
        helloARController = GameObject.FindGameObjectWithTag("PlaceController").GetComponent<GoogleARCore.Examples.HelloAR.HelloARController>();
    }

    void Update()
    {
        //if (!GameGlobal.isGameStarted)
        //  return;

        nameText.text = name;
        priceText.text = "$" + price.ToString();
        if (price > 10000)
            icon.SetIconType(MenuIconContainer.IconType.Disabled);
        else
            icon.SetIconType(MenuIconContainer.IconType.Active);
    }

    public void OnTouchBuy()
    {
        if(product == null)
        {
            Debug.Log("Brak ustawionej rzeczy do zakupu");
            return;
        }
        Debug.Log("Kupiono " + name);
        MenuOpenManager.CloseCurrent();
        helloARController.SetPrefab(product);
        helloARController.placeMode = 4;
    }
}