﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinMiner : MonoBehaviour {

    public float MinerTick = 1f; // co ile sekund ma dodac bitcoiny
    public float MinerAmount = 0.01f; // ile ma bitcoinow dodawac

    private float _time;
	void Start () {
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
}
