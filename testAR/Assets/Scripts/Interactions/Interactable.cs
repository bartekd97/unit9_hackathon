using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isInteractable;
    public string nameOfClass;
    public string nameOfMethod;
    int counter = 0;


    public void OnTap()
    {
        counter++;

        if (isInteractable && counter%2 == 0)
        {
            Type t = Type.GetType(nameOfClass);
            MethodInfo method = t.GetMethod(nameOfMethod);
            method.Invoke(GetComponent(nameOfClass), null);
        }
    }

    public void OnMouseDown()
    {
        OnTap();
    }
}
