using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainScript : MonoBehaviour {
    public EnemiesSpawnController spawner;

	void Start () {
        
    }

    public void spawn()
    {
        spawner.SpawnEnemy();
    }
}
