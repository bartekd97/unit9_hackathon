using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptureMenu : MonoBehaviour {

    public enum Mode
    {
        switchScene = 0, switchMenu= 1, exit=2, start=3, openMenu=4, closeMenu=5, toggleMenu=6
    }

    public Mode mode;

    [Tooltip("If none leave it")]
    public GameObject NextMenu;
    public GameObject Menu;

    private void Start()
    {
        if (Menu == null)
            Menu = gameObject.transform.parent.gameObject;

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

            case Mode.exit:

                Quit();

                break;

            case Mode.start:
                


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
        Debug.Log(Menu);
        Menu.SetActive(true);
    }
    public void CloseMenu()
    {
        Debug.Log(Menu);
        Menu.SetActive(false);
    }
    public void ToggleMenu()
    {
        Debug.Log(Menu);
        Menu.SetActive(!Menu.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
