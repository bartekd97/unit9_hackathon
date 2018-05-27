using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillChildren : MonoBehaviour {

    GameObject[] Objects;
    void Update () {
        /*
        foreach (Transform child in transform)
        {
            if(child.GetComponent<MeshFilter>().mesh.name[0] == 'C')
                Destroy(child.gameObject);
        }
        */

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
