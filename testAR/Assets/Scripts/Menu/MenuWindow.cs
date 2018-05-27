using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : MonoBehaviour {

    public bool isIndicator = false;
    public MenuWindow[] indicatorMenus;

    private bool _isActive = false;
    public bool isActive
    {
        get { return _isActive;  }
    }

    [HideInInspector]
    public List<MenuWindow> indicatorCallList = new List<MenuWindow>();
    private const float C_EFFECT_SPEED = 3.0f;
    private const float C_DOWN_SLIDE = 0.25f;

    private float startTime = 0f;
    private float toLevel = 0f;
    private float currentLevel = 0f;
    private float fromLevel = 0f;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private int indicatorCount = 0;

    void Start () {
        UpdatePosition(transform.localPosition);
        UpdateScale(transform.localScale);

        if (isIndicator)
        {
            foreach (MenuWindow wnd in indicatorMenus)
                wnd.AddIndicatorCall(this);
            FadeIn();
        }
    }
    public void AddIndicatorCall( MenuWindow ind )
    {
        indicatorCallList.Add(ind);
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
        if (isIndicator)
            transform.localPosition = originalPos + Vector3.up * C_DOWN_SLIDE * (1f - currentLevel);
        else
            transform.localPosition = originalPos - Vector3.up * C_DOWN_SLIDE * (1f - currentLevel);

        if (currentLevel == 0 && toLevel == 0)
            gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        if (_isActive)
            return;

        fromLevel = 0f;
        toLevel = 1f;
        //currentLevel = 0f;
        startTime = Time.time;
        gameObject.SetActive(true);
        if (!isIndicator)
        {
            currentLevel = 0f;
            foreach (MenuWindow ind in indicatorCallList)
                ind.OnWindowCallIndicator(this, true);
        }
        _isActive = true;
    }
    public void FadeOut()
    {
        if (!_isActive)
            return;

        fromLevel = 1f;
        toLevel = 0f;
        //currentLevel = 1f;
        startTime = Time.time;
        if (!isIndicator)
        {
            currentLevel = 1f;
            foreach (MenuWindow ind in indicatorCallList)
                ind.OnWindowCallIndicator(this, false);
        }
        _isActive = false;
    }

    public void OnWindowCallIndicator(MenuWindow wnd, bool state)
    {
        if (state)
            indicatorCount++;
        else
            indicatorCount--;
        if (indicatorCount == 0)
            FadeIn();
        else
            FadeOut();
    }
}
