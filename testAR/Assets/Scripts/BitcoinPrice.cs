using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinPrice : MonoBehaviour {

    public float bitcoinPrice;

    public float minPrice;
    public float maxPrice;

    public float minComp;
    public float maxComp;

    public float priceTime;

    public int calcIndex;

    private ChartController chart;

    private void Start()
    {
        chart = GameObject.Find("Chart").GetComponent<ChartController>();
        calcIndex = 0;
        StartCoroutine(CalculatingLoop());
    }

    void RandomizeCompartment()
    {
        minComp = Random.Range(minPrice, maxPrice);
        maxComp = Random.Range(minComp, maxPrice);
    }
    void RandomBitcoinPrice()
    {
        calcIndex %= 3;
        if (calcIndex == 0) RandomizeCompartment();
        
        bitcoinPrice = Random.Range(minComp, maxComp);
        GameGlobal.bitcoinExchange = bitcoinPrice;
        GameGlobal.BTCToUSD(GameGlobal.bitcoinExchange, GameGlobal.bitcoinsCurrency);
        calcIndex++;
    }
    IEnumerator CalculatingLoop()
    {
        while (true)
        {
            RandomBitcoinPrice();
            chart.DrawChart(bitcoinPrice);
            yield return new WaitForSeconds(priceTime);
        }
    }
}
