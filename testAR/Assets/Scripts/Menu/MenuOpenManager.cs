using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuOpenManager {

	public static GameObject currentOpenedMenu = null;
	
    public static void Open( GameObject menu )
    {
        if (currentOpenedMenu)
            currentOpenedMenu.SetActive(false);
        currentOpenedMenu = menu;
        currentOpenedMenu.SetActive(true);
    }
    public static void Close( GameObject menu )
    {
        menu.SetActive(false);
        if (currentOpenedMenu == menu)
            currentOpenedMenu = null;
    }
    public static void CloseCurrent()
    {
        if (currentOpenedMenu)
        {
            currentOpenedMenu.SetActive(false);
            currentOpenedMenu = null;
        }
    }
}
