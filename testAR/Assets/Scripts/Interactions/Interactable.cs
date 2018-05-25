using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isInteractable;
    public string nameOfClass;
    public string nameOfMethod;


    public void OnTap()
    {
        if (isInteractable)
        {
            Type t = Type.GetType(nameOfClass);
            MethodInfo method = t.GetMethod(nameOfMethod);
            method.Invoke(GetComponent(nameOfClass), null);
        }
    }
}
