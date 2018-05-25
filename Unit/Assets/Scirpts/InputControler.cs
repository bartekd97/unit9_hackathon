using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControler : MonoBehaviour {

    public GameObject cube;
    bool isCreated=false;
	void Update () {
        foreach(var touch in Input.touches)
        {
            Shoot(touch.position);
            if(Input.touchCount>1 && touch.phase == TouchPhase.Began && !isCreated)
            {
                AddCube();
                isCreated = true;
            }
        }
        isCreated = false;
	}

    void AddCube()
    {
        GameObject.Instantiate(cube, transform.position + transform.forward * 2f, Random.rotation);
    }

    void Shoot(Vector2 screenPoint)
    {
        var ray = Camera.main.ScreenPointToRay(screenPoint);
        var hitInfo = new RaycastHit();
        if(Physics.Raycast(ray,out hitInfo)){
            hitInfo.rigidbody.AddForceAtPosition(ray.direction, hitInfo.point);
        }
    }
}
