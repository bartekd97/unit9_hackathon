﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnController : MonoBehaviour {

    public List<GameObject> enemies = new List<GameObject>();
    public List<int> spawnRates = new List<int>();

    private int randomMax;
	void Start () {
        SetRandomMax();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject enemyPrefab = GetRandomEnemy();
            GameObject tempEnemy = Instantiate(enemyPrefab, gameObject.transform.position, enemyPrefab.transform.rotation);
            tempEnemy.GetComponent<Rigidbody>().AddForce(Vector3.forward * Random.Range(1, 5));
        }
    }
    void SetRandomMax()
    {
        randomMax = 0;
        foreach(int x in spawnRates)
        {
            randomMax += x;
        }
    }
	
    bool IsInRange(int x, int min, int max)
    {
        return (x >= min && x <= max) ? true : false;
    }

	GameObject GetRandomEnemy()
    {
        int randomNumber = Random.Range(1, randomMax);
        Debug.Log("random: " + randomNumber);
        int min = 0;
        int max = 0;
        int index = 0;
        foreach(int x in spawnRates)
        {
            max += x;
            Debug.Log("min: " + min);
            Debug.Log("max: " + max);
            if (IsInRange(randomNumber, min, max)) return enemies[index];
            else
            {
                index++;
                Debug.Log("index:" + index);
                min += x;
            }
        }
        return null;
    }
}
