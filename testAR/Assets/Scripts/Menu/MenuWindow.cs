using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : MonoBehaviour {

    private const float C_EFFECT_SPEED = 3.0f;
    private const float C_DOWN_SLIDE = 0.25f;

    private float startTime = 0f;
    private float toLevel = 0f;
    private float currentLevel = 0f;
    private float fromLevel = 0f;
    private Vector3 originalPos;
    private Vector3 originalScale;

    void Start () {
        UpdatePosition(transform.position);
        UpdateScale(transform.localScale);
    }

    public void UpdatePosition( Vector3 pos )
    {
        originalPos = pos;
    }
    public void UpdateScale(Vector3 scale)
    {
        originalScale = scale;
    }

    // Update is called once per frame
    void Update () {
        if (currentLevel == toLevel)
            return;

        float diff = toLevel - fromLevel;
        currentLevel = fromLevel + diff * (Time.time - startTime) * C_EFFECT_SPEED;
        currentLevel = Mathf.Max(0f, Mathf.Min(1f, currentLevel));

        transform.localScale = originalScale * currentLevel;
        transform.position = originalPos - Vector3.up * C_DOWN_SLIDE * (1f - currentLevel);

        if (currentLevel == 0 && toLevel == 0)
            gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        fromLevel = 0f;
        toLevel = 1f;
        currentLevel = 0f;
        startTime = Time.time;
        gameObject.SetActive(true);
    }
    public void FadeOut()
    {
        fromLevel = 1f;
        toLevel = 0f;
        currentLevel = 1f;
        startTime = Time.time;
    }
}
