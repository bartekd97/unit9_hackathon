using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

    public void hardRestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
