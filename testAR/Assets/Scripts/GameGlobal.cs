using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal {
    #region zmienne
    private static bool _isGameStarted = false;
    private static BitcoinPrice bitcoinStock;
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
    public static int playerTokens = 0; 
    public static float bitcoinsCurrency = 0;
    public static float usdCurrency = 0;
    public static float bitcoinExchange;
    #endregion
    public static void AddCash(string currency, float amount)
    {
        bitcoinExchange = bitcoinStock.bitcoinPrice;
        BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        if(currency.ToLower() == "usd" || currency.ToLower() == "dollars" || currency == "$")
        {
            usdCurrency += amount;
            USDToBTC(bitcoinExchange, usdCurrency);
        }else if(currency.ToLower() == "btc" || currency.ToLower() == "bitcoins" || currency.ToLower() == "bitcoin")
        {
            bitcoinsCurrency += amount;
            BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        }
    }

    public static void SubtractCash(string currency, float amount)
    {
        BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        if (currency.ToLower() == "usd" || currency.ToLower() == "dollars" || currency == "$")
        {
            usdCurrency -= amount;
            USDToBTC(bitcoinExchange, usdCurrency);
        }
        else if (currency.ToLower() == "btc" || currency.ToLower() == "bitcoins" || currency.ToLower() == "bitcoin")
        {
            bitcoinsCurrency -= amount;
            BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        }
    }

    public static void USDToBTC(float btcExchange, float cash)
    {
        bitcoinsCurrency = cash / btcExchange;
    }

    public static void BTCToUSD(float btcExchange, float cash)
    {
        usdCurrency = bitcoinsCurrency * btcExchange;
    }

    public static void CountEnemies()
    {
        enemiesOnTheScene = GameObject.Find("Enemies").transform.childCount;
    }

    public static void StartGame(GameObject bitcoinMiner, Vector3 minerPosition, Quaternion minerRotation, Transform parent)
    {
        if (_isGameStarted)
            return;

       _bitcoinMiner = (GameObject.Instantiate(bitcoinMiner, minerPosition, minerRotation, parent) as GameObject).GetComponent<BitcoinMiner>();
       _bitcoinMiner = bitcoinMiner.GetComponent<BitcoinMiner>();
        bitcoinsCurrency = 0;

        _isGameStarted = true;
    }
}
