using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

    public float duration;
    public Image img;
    public bool b;

    private float time;

    public void ChangeBool()
    {
        b = !b;
    }

    void Update () {
        if (b)
        {
            if (time * 2 < duration)
                img.color = new Color(img.color.r, img.color.g, img.color.b, time / duration);
            else
                img.color = new Color(img.color.r, img.color.g, img.color.b, ((duration-time) / duration ));

            time += Time.deltaTime;
            if (time > duration)
            {
                b = false;
                time = 0;
            }
        }
	}
}
