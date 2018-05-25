using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptureMenu : MonoBehaviour {

    public enum Mode
    {
        switchScene = 0, switchMenu= 1, exit=2
    }

    public Mode mode;

    [Tooltip("If none leave it")]
    public GameObject NextMenu;
    public GameObject Menu;

    private void Start()
    {
        Menu = gameObject.transform.parent.gameObject;
    }

    [Tooltip("If none leave it")]
    public string NextScene;

    public void Capture()
    {
        switch(mode)
        {
            case Mode.switchScene:
                SwitchScene();
                break;
            case Mode.switchMenu:
                SwitchMenu();
                break;
            case Mode.exit:
                Quit();
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

    public void Quit()
    {
        Application.Quit();
    }
}
