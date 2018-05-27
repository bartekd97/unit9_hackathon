using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour {

    public Transform rotatable;
    public float minDistanceToShoot = 2f;
    public SentryGunController gun;
    public float addRot = 0f;

    const float CHECK_TICK = 0.5f;
    float lastTime;
	void Start () {
        lastTime = Time.time;
    }
	
	void Update () {
		if (Time.time - lastTime >= CHECK_TICK)
        {
            lastTime = Time.time;
            if (gun.timeoutLeft <= 0f)
            {
                Vector3 myPos = transform.position;

                float minDistance = -1f;
                Transform nearest = null;
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    float dist = (go.transform.position - myPos).magnitude;
                    if (dist < minDistance || minDistance == -1f)
                    {
                        minDistance = dist;
                        nearest = go.transform;
                    }
                }
                if (nearest != null && minDistance <= minDistanceToShoot)
                {
                    float atg = Mathf.Atan2(nearest.position.x - rotatable.position.x, nearest.position.z - rotatable.position.z);
                    rotatable.rotation = Quaternion.Euler(0, atg * Mathf.Rad2Deg + addRot, 0);
                    gun.Shoot();
                    lastTime = Time.time + 1f;
                }
            }
        }
	}
}
