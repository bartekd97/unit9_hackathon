using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuOpenManager {

	public static GameObject currentOpenedMenu = null;
	
    public static void Open( GameObject menu )
    {
        if (currentOpenedMenu)
        {
            //currentOpenedMenu.SetActive(false);
            currentOpenedMenu.GetComponent<MenuWindow>().FadeOut();
        }
        currentOpenedMenu = menu;
        //currentOpenedMenu.SetActive(true);
        currentOpenedMenu.GetComponent<MenuWindow>().FadeIn();
    }
    public static void Close( GameObject menu )
    {
        //menu.SetActive(false);
        menu.GetComponent<MenuWindow>().FadeOut();
        if (currentOpenedMenu == menu)
            currentOpenedMenu = null;
    }
    public static void CloseCurrent()
    {
        if (currentOpenedMenu != null)
        {
            //currentOpenedMenu.SetActive(false);
            currentOpenedMenu.GetComponent<MenuWindow>().FadeOut();
            currentOpenedMenu = null;
        }
    }
}
