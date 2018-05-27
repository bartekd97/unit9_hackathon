using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnController : MonoBehaviour {

    public List<GameObject> enemies = new List<GameObject>();
    public List<int> spawnRates = new List<int>();

    public float spawnTime = 5f;

    private int randomMax;
    private GameObject enemiesParent;
    private float lastTime;
	void Start () {
        SetRandomMax();
        enemiesParent = GameObject.Find("Enemies");
        lastTime = Time.time;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy();
        }
        */
        if (Time.time - lastTime >= spawnTime)
        {
            lastTime = Time.time;
            SpawnEnemy();
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

    public void SpawnEnemy()
    {
        if (GameGlobal.enemiesOnTheScene >= GameGlobal.MaxEnemiesOnScene)
            return;
        GameObject enemyPrefab = GetRandomEnemy();
        if (enemyPrefab != null)
        {
            GameObject tempEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation, enemiesParent.transform);
            //tempEnemy.GetComponent<Rigidbody>().AddForce(Vector3.forward * Random.Range(1, 5));
            GameGlobal.CountEnemies();
        }
    }
	GameObject GetRandomEnemy()
    {
        int randomNumber = Random.Range(1, randomMax);
        int min = 0;
        int max = 0;
        int index = 0;
        foreach(int x in spawnRates)
        {
            max += x;
            if (IsInRange(randomNumber, min, max)) return enemies[index];
            else
            {
                index++;
                min += x;
            }
        }
        return null;
    }
}
