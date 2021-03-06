﻿//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Reflection;
    using System;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject AndyAndroidPrefab;

        public GameObject DefenseUnitPrefab;

        /// <summary>
        /// A gameobject parenting UI for displaying the "searching for planes" snackbar.
        /// </summary>
        public GameObject SearchingForPlaneUI;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 180.0f;

        /// <summary>
        /// A list to hold all planes ARCore is tracking in the current frame. This object is used across
        /// the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        /// 
        [SerializeField]
        bool check = false;
        //Pole którym sprawdzamy czy gracz kliknął pierwszy raz, jeżeli tak to kładzie bazę. Jeżeli nie to kładzie jednostkę do obrony
        
        public int placeMode;

        [SerializeField]
        Image laserCrosshair;

        [SerializeField]
        bool placeMenu;

        public GameObject spawn;

        public GameObject menu;
        
        public Text tekst;
        public Text info;

        public bool ghost = false;

        public Gamemanager gamemanager;
        public GameObject planeGenerator;
        public music Music;
        public GameObject InfoPrefab;
        public GameObject Reset;
        private void Start()
        {
            GameGlobal.AddCash("btc", 2);
            //spawn = DefenseUnitPrefab;
        }

        public void SetPrefab(GameObject item)
        {
            DefenseUnitPrefab = item;
        }

        void PlaceGhostObject()
        {
            if (ghost)
            {
                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                   TrackableHitFlags.FeaturePointWithSurfaceNormal;
                if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
                {

                    // Use hit pose and camera pose to check if hittest is from the
                    // back of the plane, if it is, no need to create the anchor.
                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else
                    {
                        var andyObject = Instantiate(DefenseUnitPrefab, hit.Pose.position, hit.Pose.rotation);


                        //GameGlobal.StartGame(andyObject);
                        // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
                        andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                        // world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make Andy model a child of the anchor.
                        andyObject.transform.parent = anchor.transform;

                        Destroy(andyObject);

                    }
                }
            }
        }

        

        public void Update()
        {
            _UpdateApplicationLifecycle();
            //PlaceGhostObject();
            // Hide snackbar when currently tracking at least one plane.
            Session.GetTrackables<DetectedPlane>(m_AllPlanes);
            bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++)
            {
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                {
                    showSearchingUI = false;
                    break;
                }
            }
            /*
            if (placeMode == 1)
                laserCrosshair.enabled = true;
            else
                laserCrosshair.enabled = false;
                */
            SearchingForPlaneUI.SetActive(showSearchingUI);
            tekst.text = Input.GetTouch(0).ToString();

            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }
            
            
             // Raycast against the location the player touched to search for planes.
                
                switch (placeMode)
                {
                    case 0:
                        spawn = AndyAndroidPrefab;
                        ustawBudynki(spawn);
                        planeGenerator.GetComponent<SpawnTeleports>().StartSpawning();
                        placeMode = 3;
                        ghost = true;
                        Music.StartMusic();
                        Reset.SetActive(false);
                        Time();
                        info.text = "Click on base to buy buildings";
                        // inputControler.enabled = true;
                        //placeMode = 4;
                        //ustawianie koparki
                        break;
                    case 1:
                        laser();
                        
                        //laser XD
                        break;
                    case 2:
                        spawn = menu;
                        ustawBudynki(spawn);
                        placeMode = 3;
                        gamemanager.disableButton();
                        info.text = "Now choose your game mode";
                        //spaceForMenu.GetComponent<CaptureMenu>().SetText("Choose any place for menu");
                        //ustawianie menu
                        break;
                    case 3:
                        Shoot(touch.position);
                        break;
                    case 4:
                        spawn = DefenseUnitPrefab;
                        ustawBudynki(spawn);
                        placeMode = 3;
                        //spaceForMenu.GetComponent<CaptureMenu>().SetText("Choose any place for buildings");
                        //ustawianie obrony
                        break;
                }
            
        }

        

        void Shoot(Vector2 screenPoint)
        {
            var ray = Camera.main.ScreenPointToRay(screenPoint);
            var hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                var script = hitInfo.transform.gameObject.GetComponent("Interactable");
                if (script != null)
                {
                    Type t = Type.GetType("Interactable");
                    MethodInfo method = t.GetMethod("OnTap");
                    method.Invoke(script, null);
                }
                else
                {
                    MenuOpenManager.CloseCurrent();
                }
            }
        }

        void setGameObject(GameObject Object)
        {
            spawn = Object;
        }

        void ustawBudynki(GameObject spawn)
        {
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
               TrackableHitFlags.FeaturePointWithSurfaceNormal;
            if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
            {

                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                        var andyObject = Instantiate(spawn, hit.Pose.position, hit.Pose.rotation);

                    
                        //GameGlobal.StartGame(andyObject);
                    // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
                        andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                        // world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make Andy model a child of the anchor.
                        andyObject.transform.parent = anchor.transform;
                                            
                }
            }
        }

        void laser()
        {
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
               TrackableHitFlags.FeaturePointWithSurfaceNormal;
            TrackableHit hit;
            if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
            {

                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    
                        var defenseUnit = Instantiate(DefenseUnitPrefab, hit.Pose.position, hit.Pose.rotation);

                        defenseUnit.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        defenseUnit.transform.parent = anchor.transform;

                        laserCrosshair.enabled = false;

                    GetComponent<HelloARController>().enabled = false;
                }
            }
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        public IEnumerator Time()
        {
            yield return new WaitForSeconds(5f);
            InfoPrefab.SetActive(false);

        }
        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
