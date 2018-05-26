using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

public class InputControler : MonoBehaviour {

    public GoogleARCore.Examples.HelloAR.HelloARController helloARController;

    public void Start()
    {
       helloARController.placeMode = 3;
       helloARController.enabled = false;
    }

    void Update() {
        foreach (var touch in Input.touches)
        {
            Shoot(touch.position);
        }
       // int a = (0 == 0) ? 0 : 2;
    }

    void Shoot(Vector2 screenPoint)
    {
        var ray = Camera.main.ScreenPointToRay(screenPoint);
        var hitInfo = new RaycastHit();
        if(Physics.Raycast(ray,out hitInfo)){
            var script = hitInfo.transform.gameObject.GetComponent("Interactable");
            if(script != null)
            {
                Type t = Type.GetType("Interactable");
                MethodInfo method = t.GetMethod("OnTap");
                method.Invoke(script, null);
            }
        }
    }
}
