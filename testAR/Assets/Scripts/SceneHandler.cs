﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public void ReloadScene()
    {
        LoadScene(1);
    }
}
