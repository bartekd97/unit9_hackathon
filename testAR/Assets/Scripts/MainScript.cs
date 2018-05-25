using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {
    public GameObject bitcoinMinerPrefab;

	void Start () {
        GameGlobal.StartGame(bitcoinMinerPrefab, transform, Vector3.forward*20, Quaternion.identity);
    }
}
