using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinMiner : MonoBehaviour {

    public float MinerTick = 1f; // co ile sekund ma dodac bitcoiny
    public float MinerAmount = 0.01f; // ile ma bitcoinow dodawac
    public GameObject sparksParticles;

    private float _time;
	void Start () {
        SetParticlesState(false);
        _time = Time.time;
	}
	void Update () {
        if (!GameGlobal.isGameStarted)
            return;

        float dt = Time.time - _time;
        if (dt >= MinerTick)
        {
            _time = Time.time;
            GameGlobal.bitcoinsCurrency += MinerAmount;
        }
	}
    public void SetParticlesState(bool active)
    {
        sparksParticles.SetActive(active);
    }
    public void LeveledUp()
    {
        if(gameObject.GetComponent<LevelController>().currentLevel > 2) MinerTick /= gameObject.GetComponent<LevelController>().currentLevel - 1;
        MinerTick *= gameObject.GetComponent<LevelController>().currentLevel;
    }
}
