using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChild : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.parent.gameObject.GetComponent<LaserRayController>().OnChildTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.parent.gameObject.GetComponent<LaserRayController>().OnChildTriggerExit(other);
    }
}
