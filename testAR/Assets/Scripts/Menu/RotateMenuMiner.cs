using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuMiner : MonoBehaviour {
    public float speed = 2f;
	void Update () {
        transform.RotateAround(transform.position, transform.forward, speed);
	}
}
