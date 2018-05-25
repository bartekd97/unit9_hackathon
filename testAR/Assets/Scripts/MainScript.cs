using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {
    public GameObject bitcoinMinerPrefab;
    public Transform robotPortal;
    public GameObject robotPrefab;

	void Start () {
        GameGlobal.StartGame(bitcoinMinerPrefab, transform, Vector3.forward*20, Quaternion.identity);
        Instantiate(robotPrefab, robotPortal.position, robotPortal.rotation, robotPortal);
    }
}
