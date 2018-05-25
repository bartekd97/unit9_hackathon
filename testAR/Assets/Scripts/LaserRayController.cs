using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayController : MonoBehaviour {

    [HideInInspector]
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            // Deal with enemy health script or whatever
        }
    }
}
