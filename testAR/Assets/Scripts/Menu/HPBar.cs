using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    public Image HealthImage;
    public RectTransform HealthRect;
    public Health ObjectHealth;

    public float xOffset = 0.45f;
    public float alpha = 0.75f;

    private Vector3 originPos;
    private Vector2 originSize;
    void Start () {
        originPos = HealthRect.localPosition;
        originSize = HealthRect.sizeDelta;
    }
	
	void Update () {
        if (!ObjectHealth)
            return;

        float hpLevel = ObjectHealth.currentHealth / ObjectHealth.maxHealth;
        float rectX = xOffset - xOffset * hpLevel;
        float rectWidth = 100f * hpLevel;

        Vector3 pos = originPos;
        pos.x = rectX;
        HealthRect.localPosition = pos;
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
        HealthImage.color = new Color(r * 0.5f + 0.5f, g * 0.5f + 0.5f, 0.5f, alpha);
    }
}
