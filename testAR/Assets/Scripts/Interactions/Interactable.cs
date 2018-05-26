using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isInteractable;
    public string nameOfClass;
    public string nameOfMethod;
    public AudioClip sfx;
    int counter = 0;

    private AudioSource asource;
    private void Start()
    {
        if (sfx)
        {
            asource = gameObject.AddComponent<AudioSource>();
            asource.playOnAwake = true;
        }
    }

    public void OnTap()
    {
        counter++;

        if (isInteractable && counter%2 == 0)
        {
            Type t = Type.GetType(nameOfClass);
            MethodInfo method = t.GetMethod(nameOfMethod);
            method.Invoke(GetComponent(nameOfClass), null);
            if (sfx)
            {
                asource.PlayOneShot(sfx);
            }
        }
        Debug.Log("On Tap!");
    }

    public void OnMouseDown()
    {
        OnTap();
        Debug.Log("On Mouse Down!");
    }
}
