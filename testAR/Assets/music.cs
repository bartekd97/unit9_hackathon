using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {

    public AudioClip[] Music;
    public AudioSource source;
    public int track;
    public bool check = false;
    
    public void StartMusic () {
        track = Random.Range(0, Music.Length);
        source.clip = Music[track];
        source.Play();
        check = true;
    }
    private void Update()
    {
        if (check)
        {
            if (!source.isPlaying)
            {
                track = Random.Range(0, Music.Length);
                source.clip = Music[track];
                source.Play();
            }
        }
    }

}
