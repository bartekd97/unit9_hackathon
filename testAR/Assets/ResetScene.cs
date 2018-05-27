using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

    public GoogleARCore.Examples.Common.DetectedPlaneGenerator detectedPlaneGenerator;
    public void hardRestartGame()
    {
        detectedPlaneGenerator.m_NewPlanes.Clear();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
