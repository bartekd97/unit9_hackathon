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

        public DetectedPlaneGenerator planeGenerator;
        public HelloARController helloARController;
        public Button stopScanTerrain;
        public GameObject Canvas;
        public GameObject stopScanTerrainObject;
        public Button[] buttons;
        public InputControler inputControler;
        // Use this for initialization
        void Start()
        {
            planeGenerator.enabled = true;
            helloARController.enabled = false;
            Canvas = GameObject.FindGameObjectWithTag("Canvas");
            findButons();
            stopScanTerrain.onClick.AddListener(disableScanning);
            GetComponent<Gamemanager>().enabled = false;
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
            helloARController.enabled = true;
            helloARController.placeMode = 0;
            planeGenerator.enabled = false;
        }



        void disableScanning()
        {
            planeGenerator.enabled = false;
            helloARController.enabled = true;
            stopScanTerrainObject.SetActive(false);
            helloARController.placeMode = 2;
        }

    }
}
