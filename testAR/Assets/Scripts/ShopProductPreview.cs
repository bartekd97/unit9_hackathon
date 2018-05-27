using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public class ShopProductPreview : MonoBehaviour {
    private Camera FirstPersonCamera;
    private const float k_ModelRotation = 180.0f;
    private void Start()
    {
        FirstPersonCamera = Camera.main;
    }
    void Update () {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
           TrackableHitFlags.FeaturePointWithSurfaceNormal;
        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {

            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (!((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0))
            {
                transform.position = hit.Pose.position;
                transform.rotation = hit.Pose.rotation;
                transform.Rotate(0, k_ModelRotation, 0, Space.Self);
            }
        }

        Touch touch;

        if (GoogleARCore.InstantPreviewInput.touchCount < 1 || (touch = GoogleARCore.InstantPreviewInput.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        Destroy(gameObject);
    }
}
