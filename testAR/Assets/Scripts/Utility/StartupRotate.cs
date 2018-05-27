using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupRotate : MonoBehaviour {

    public Vector3 rotate;
	void Start () {
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation + rotate);
	}
}
