using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal {
    #region zmienne
    
    //private static bool _isGameStarted = false;
    public static bool isGameStarted
    {
        get
        {
            return true;// _isGameStarted;
        }
    }
    //private static BitcoinMiner _bitcoinMiner;
    public static BitcoinMiner bitcoinMiner
    {
        get
        {
            return GameObject.Find("BitcoinMiner").GetComponent<BitcoinMiner>();//_bitcoinMiner;
        }
    }

    public static int MaxEnemiesOnScene = 5;

    public static int enemiesOnTheScene = -1;
    public static int playerTokens = 0;
    public static float bitcoinsCurrency = 2;
    public static float usdCurrency = 0;
    public static float bitcoinExchange;
    public static int currentLevel = 1;
    public static int totalExperience = 0;
    #endregion
    public static void AddCash(string currency, float amount)
    {
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
    public static void RecalculateCash()
    {
        BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        USDToBTC(bitcoinExchange, usdCurrency);
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
    public static bool CanBuy(float dollarPrice)
    {
        BTCToUSD(bitcoinExchange, bitcoinsCurrency);
        return dollarPrice < usdCurrency ? true : false;
    }
    /*
    public static void StartGame(GameObject bitcoinMiner)
    {
        if (_isGameStarted)
            return;

       _bitcoinMiner = bitcoinMiner.GetComponent<BitcoinMiner>();

        bitcoinsCurrency = 0;
        currentLevel = 1;
        totalExperience = 0;

        _isGameStarted = true;
    }
    public static void StopGame()
    {
        if (!_isGameStarted)
            return;

        _bitcoinMiner = null;

        _isGameStarted = false;
    }
    */
}
