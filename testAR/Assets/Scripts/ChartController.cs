using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartController : MonoBehaviour {

    public float zeroLine;
    public int points;
    public float z;

    private int index;
    private LineRenderer lRenderer;
	
	void Awake () {
        lRenderer = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<LineRenderer>();
        lRenderer.positionCount = points;
	}

    Vector3 LinePointFromYValue(float value)
    {
        Vector3 res = new Vector3(9, (value-zeroLine)/500, z);
        return res;
    }
	
	public void DrawChart(float value)
    {
        //Move the points
        for (int i = 1; i < lRenderer.positionCount; i++)
        {
            Vector3 movedPoint = new Vector3(i-1, lRenderer.GetPosition(i).y, z);
            lRenderer.SetPosition(i - 1, movedPoint);
        }
        lRenderer.SetPosition(9, LinePointFromYValue(value));
    }
}
