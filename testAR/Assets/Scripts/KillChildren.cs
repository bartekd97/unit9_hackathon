using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillChildren : MonoBehaviour {

    void Update () {
        int i = 0;
	    foreach(Transform child in transform)
        {
            if(i++ != 0)
            {
                Destroy(child.gameObject);
            }
        }
	}
}
