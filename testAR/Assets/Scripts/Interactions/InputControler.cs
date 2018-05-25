using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

public class InputControler : MonoBehaviour {

    //public GameObject DebugGO;
    //public Material DebugMat;
    public Text DebugText;

    bool isCreated = false;
    void Update() {
        foreach (var touch in Input.touches)
        {
            Shoot(touch.position);
        }
        isCreated = false;
    }

    void Shoot(Vector2 screenPoint)
    {
        var ray = Camera.main.ScreenPointToRay(screenPoint);
        var hitInfo = new RaycastHit();
        if(Physics.Raycast(ray,out hitInfo)){
            var script = hitInfo.transform.gameObject.GetComponent("Interactable");
            if(script != null)
            {
                //DebugGO.GetComponent<MeshRenderer>().material = DebugMat;
                /*if(hitInfo.transform.gameObject.name == "" || hitInfo.transform.gameObject.name==null)
                {
                    DebugText.text = "lol, crashed";
                }
                else
                {
                    DebugText.text = hitInfo.transform.gameObject.name;
                }*/

                Type t = Type.GetType("Interactable");
                MethodInfo method = t.GetMethod("OnTap");
                method.Invoke(script, null);
            }
        }
    }
}
