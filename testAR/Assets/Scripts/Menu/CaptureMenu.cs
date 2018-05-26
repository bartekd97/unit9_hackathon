using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaptureMenu : MonoBehaviour {

    public enum Mode
    {
        switchScene = 0, switchMenu= 1, exit=2, start=3, openMenu=4, closeMenu=5, toggleMenu=6, nextMenu=7
    }

    public Mode mode;

    [Tooltip("If none leave it")]
    public GameObject NextMenu;
    public GameObject Menu;
    public Text info;

    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<Text>();
        info.text = "Now place your Bitcoin miner base";
        if (Menu == null)
            Menu = gameObject.transform.parent.gameObject;

    }


    public void SetText(string text)
    {
        info.text = text;
    }

    [Tooltip("If none leave it")]
    public string NextScene;

    public void Capture()
    {
        switch(mode)
        {
            case Mode.switchScene:

                if (NextScene == null)
                {
                    return;
                }
                SwitchScene();

                break;

            case Mode.switchMenu:

                if (NextMenu == null)
                {
                    return;
                }
                if (Menu == null)
                {
                    return;
                }

                SwitchMenu();

                break;

            case Mode.openMenu:
                if (Menu == null)
                {
                    return;
                }
                OpenMenu();
                break;
            case Mode.closeMenu:
                if (Menu == null)
                {
                    return;
                }
                CloseMenu();
                break;
            case Mode.toggleMenu:
                if (Menu == null)
                {
                    return;
                }
                ToggleMenu();
                break;
            case Mode.nextMenu:
                if (NextMenu == null)
                {
                    return;
                }
                if (Menu == null)
                {
                    return;
                }
                DoNextMenu();
                break;

            case Mode.exit:

                Quit();

                break;

            case Mode.start:

                GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
                manager.GetComponent<GoogleARCore.Examples.HelloAR.Gamemanager>().StartGame();
                GameObject menu = gameObject.transform.parent.gameObject;
                menu.SetActive(false);
                info.text = "Click on base to buy buildings";
                break;
        }
    }
    
    public void SwitchMenu()
    {
        Debug.Log(Menu.name);
        Instantiate(NextMenu, gameObject.transform.parent.position, Quaternion.identity);
        Destroy(Menu);
    }

    public void SwitchScene()
    {
        Debug.Log(NextScene);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }

    public void OpenMenu()
    {
        MenuOpenManager.Open(Menu);
        info.text = "Select what you want!";
    }
    public void CloseMenu()
    {
        MenuOpenManager.Close(Menu);
        info.text = "Do what you want!";
    }
    public void ToggleMenu()
    {
        Debug.Log(Menu);
        if (Menu.activeSelf)
        {
            MenuOpenManager.Close(Menu);
            info.text = "Do what you want!";
        }
        else
        {
            MenuOpenManager.Open(Menu);
            info.text = "Select what you want!";
        }
    }
    public void DoNextMenu()
    {
        Debug.Log(Menu);
        MenuOpenManager.Close(Menu);
        MenuOpenManager.Open(NextMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
