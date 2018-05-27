using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimation : MonoBehaviour {

    public Animator ator;

    void Start () {
        ator = GetComponent<Animator>();	
	}

    public void SetBoolean(string nameOfBool, bool value)
    {
        ator.SetBool(nameOfBool, value);
    }

    public void SwitchBoolean(string nameOfBool, bool value)
    {
        ator.SetBool(nameOfBool, !ator.GetBool(nameOfBool));
    }
}
