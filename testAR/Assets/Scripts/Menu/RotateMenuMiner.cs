using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuMiner : MonoBehaviour {
	void Update () {
        transform.RotateAround(transform.position, transform.forward, 2f);
	}
}
