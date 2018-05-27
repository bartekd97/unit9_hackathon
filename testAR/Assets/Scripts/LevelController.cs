using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public int currentLevel = 1;
    private int neededExperience = 50;
    public int experience;
	void Start () {
        currentLevel = 1;
	}
    public void AddExperience(int amount)
    {
        experience += amount;
        GameGlobal.totalExperience += amount;
        if(experience - neededExperience >= 0)
        {
            LevelUp();
        }
    }
	public float LevelSliderValue()
    {
        return (float)experience / (float)neededExperience;
    }
    void LevelUp()
    {
        experience -= neededExperience;
        currentLevel++;
        GameGlobal.currentExperience = experience;
        neededExperience = Mathf.RoundToInt((float)neededExperience * 1.23f);
        GameGlobal.playerTokens++;
        GameGlobal.currentLevel = currentLevel;
        if(gameObject.tag == "BitcoinMiner")
        {
            gameObject.GetComponent<BitcoinMiner>().LeveledUp();
        }
    }
}
