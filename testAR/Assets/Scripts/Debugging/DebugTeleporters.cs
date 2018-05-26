using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleporters : MonoBehaviour {

    private GameObject plane;
    
    private void Update()
    {
        plane = gameObject.transform.GetChild(0).gameObject;
        if (plane != null)
        {
            int i = 0;
            foreach (Vector3 v3 in plane.GetComponent<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>().m_MeshVertices)
            {
                Debug.Log(i +" "+ v3.x+" "+v3.y+" "+v3.z);
                i++;
            }
            i = 0;
        }
    }
}
