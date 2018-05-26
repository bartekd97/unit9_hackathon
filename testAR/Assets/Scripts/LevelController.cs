using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public int currentLevel = 1;
    private int neededExperience = 50;
    public int experience;
	void Start () {
		
	}
    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience - neededExperience > 0)
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
        neededExperience = Mathf.RoundToInt((float)neededExperience * 1.23f);
        if(gameObject.tag == "BitcoinMiner")
        {
            gameObject.GetComponent<BitcoinMiner>().LeveledUp();
        }
    }
}
