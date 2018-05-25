using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public bool isActive;
    public Transform target;
	void Update () {
        if (isActive)
            transform.LookAt(target);
	}
}
