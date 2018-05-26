using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerIndicatorUpdater : MonoBehaviour {

    public Text LevelXP;
    public Text Tokens;
    public Text BTC;
    public Image HealthImage;
    public RectTransform HealthRect;
    public Health MinerHealth;

    private Vector3 originPos;
    private Vector2 originSize;
    private void Start()
    {
        originPos = HealthRect.position;
        originSize = HealthRect.sizeDelta;
    }

    void Update ()
    {
        LevelXP.text = "Level: 3\nXP: 12345";
        Tokens.text = "Available tokens: 3";
        BTC.text = "Currency: 1,237 BTC\n1 BTC = 23,456$";

        float hpLevel = MinerHealth.currentHealth / MinerHealth.maxHealth;
        float rectX = 0.35f - 0.35f * hpLevel;
        float rectWidth = 100f * hpLevel;

        Vector3 pos = originPos;
        pos.x = -rectX;
        HealthRect.position = pos;
        Vector2 size = originSize;
        size.x = rectWidth;
        HealthRect.sizeDelta = size;

        float r, g;
        if (hpLevel > 0.5f)
        {
            g = 1f;
            r = (1f - hpLevel) * 2f;
        }
        else
        {
            g = hpLevel * 2f;
            r = 1f;
        }
        HealthImage.color = new Color(r * 0.5f + 0.5f, g * 0.5f + 0.5f, 0.5f, 0.75f);
	}
}
