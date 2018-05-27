using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupItem : MonoBehaviour
{

    public string name;
    public int tokens;
    public Text nameText;
    public Text priceText;
    public MenuIconContainer icon;
    // public dzialko dzialko // trzeba dodac jeszcze zeby do tego itemu przypisac dzialko zeby wiedziec ktoremu kupujemy powerup
    // albo zachowac gdzies indziej obecnie otwarte dzialko (patrz nizej)

    void Update()
    {
        //if (!GameGlobal.isGameStarted)
        //  return;

        if (name == "LiquoredUp")
        {
            nameText.text = name;
            priceText.text = "Soon..";
            icon.SetIconType(MenuIconContainer.IconType.Disabled);
        }
        else
        {
            nameText.text = name;
            priceText.text = "Tokens: " + tokens.ToString();
            if (tokens > GameGlobal.playerTokens)
                icon.SetIconType(MenuIconContainer.IconType.Disabled);
            else if (false) // tu mozemy zrobic ze jak powerup jest uzyty to go podswietla i blokuje mu klikanie
                icon.SetIconType(MenuIconContainer.IconType.Hover);
            else
                icon.SetIconType(MenuIconContainer.IconType.Active);
        }
    }

    public void OnTouchBuy()
    {
        Debug.Log("Kupiono powerup " + name);
        string powerup = name;
        GameGlobal.playerTokens -= tokens;
        GameObject sentryParent = gameObject.transform.parent.parent.parent.gameObject;

        sentryParent.GetComponent<SentryGunController>().GetPowerUp(powerup);
        
        //MenuOpenManager.currentOpenedMenu; // zminenna oznacza obiekt obecnie otwartego menu
        // w tym przypadku to bedzie menu Powerups w menu dzialka wiec mozna na podstawie tego okreslic obecne dzialko i przyznac powerupa
        MenuOpenManager.CloseCurrent();
    }
}
