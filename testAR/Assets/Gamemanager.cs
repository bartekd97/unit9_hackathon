using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

namespace GoogleARCore.Examples.HelloAR
{

    public class Gamemanager : MonoBehaviour
    {
        public DetectedPlaneGenerator detectedPlaneGenerator;
        public HelloARController helloARController;
        public Button stopScanTerrain;
        public GameObject Canvas;
        public GameObject stopScanTerrainObject;
        public Button[] buttons;
        public Text info;
       
        // Use this for initialization
        void Start()
        {
            Canvas = GameObject.FindGameObjectWithTag("Canvas");
            findButons();
            stopScanTerrain.onClick.AddListener(disableScanning);
            info.text = "Move your phone to detect the foor, then click next";
        }

        // Update is called once per frame
        void Update()
        {

        }

        void findButons()
        {
            stopScanTerrainObject = GameObject.FindGameObjectWithTag("StartButton");
            buttons = Canvas.GetComponentsInChildren<Button>();
            int size = buttons.Length;
            for (int i = 0; i < size; i++)
            {
                if(buttons[i].tag == "StartButton")
                {
                    stopScanTerrain = buttons[i];
                }
                
            }

        }

        public void StartGame()
        {
            helloARController.placeMode = 0;
        }



        void disableScanning()
        {
            //info.text = "Now place your menu buttons on floor";
            detectedPlaneGenerator.enabled = false;
            DetectedPlaneVisualizer[] plane = FindObjectsOfType<DetectedPlaneVisualizer>();
            for (int i = 0; i < plane.Length; i++)
            {
                plane[i].enabled = false;
            }
            helloARController.enabled = true;
            helloARController.placeMode = 2;
            stopScanTerrainObject.SetActive(false);
        }

    }
}
