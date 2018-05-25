using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal {
    #region zmienne
    private static bool _isGameStarted = false;

    public static bool isGameStarted
    {
        get
        {
            return _isGameStarted;
        }
    }
    private static BitcoinMiner _bitcoinMiner;
    public static BitcoinMiner bitcoinMiner
    {
        get
        {
            return _bitcoinMiner;
        }
    }

    public static int enemiesOnTheScene = -1;
    public static int bitcoinsCurrency = 0;
    #endregion

    public static void CountEnemies()
    {
        enemiesOnTheScene = GameObject.Find("Enemies").transform.childCount;
    }

    public static void StartGame( GameObject miner, Transform parent, Vector3 minerPosition, Quaternion minerRotation )
    {
        if (_isGameStarted)
            return;

        _bitcoinMiner = (GameObject.Instantiate(miner, minerPosition, minerRotation, parent) as GameObject).GetComponent<BitcoinMiner>();
        bitcoinsCurrency = 0;

        _isGameStarted = true;
    }
}
