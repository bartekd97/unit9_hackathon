using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClick : MonoBehaviour {

    Animator animat;

	// Use this for initialization
	void Start () {
        animat = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animat.SetBool("isFighting", !animat.GetBool("isFighting"));
        }
	}
}
