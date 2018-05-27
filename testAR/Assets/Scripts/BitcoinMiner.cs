using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitcoinMiner : MonoBehaviour {

    public float MinerTick = 1f; // co ile sekund ma dodac bitcoiny
    public float MinerAmount = 0.01f; // ile ma bitcoinow dodawac
    public GameObject gameOverMenu;
    public GameObject sparks;
    public bool losed;

    private float _time;
	void Start () {
        losed = false;
        _time = Time.time;
        SetParticlesState(false);
	}
	void Update () {
        if (!GameGlobal.isGameStarted)
            return;

        float dt = Time.time - _time;
        if (dt >= MinerTick && !losed)
        {
            _time = Time.time;
            GameGlobal.bitcoinsCurrency += MinerAmount;
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    GameOver();
        //}
	}

    public void SetParticlesState(bool active)
    {
        sparks.SetActive(active);
    }
    public void GameOver()
    {
        if (!losed)
        {
            //Get cam pos
            Vector3 camPos = Camera.main.transform.position;
            Vector3 endScreenPos = new Vector3(camPos.x - 1f, camPos.y - 0.3f, camPos.z);
            GameObject gameOverBitcoins = Instantiate(gameOverMenu, endScreenPos, Camera.main.transform.rotation);
            losed = true;
        }
        LoadSceneAfterDeath();

    }
    public void LeveledUp()
    {
        if(gameObject.GetComponent<LevelController>().currentLevel > 2) MinerTick /= gameObject.GetComponent<LevelController>().currentLevel - 1;
        MinerTick *= gameObject.GetComponent<LevelController>().currentLevel;
    }
    public IEnumerator LoadSceneAfterDeath()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
     }
}
