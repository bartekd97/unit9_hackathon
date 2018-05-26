using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public bool isActive;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    void Update () {
        if (isActive)
            transform.LookAt(cam.transform);
	}
}
