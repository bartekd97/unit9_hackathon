using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainScript : MonoBehaviour {
    public GameObject bitcoinMinerPrefab;

	void Awake () {
        GameGlobal.StartGame(bitcoinMinerPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        //Instantiate(robotPrefab, robotPortal.position, robotPortal.rotation, robotPortal);
    }
}
