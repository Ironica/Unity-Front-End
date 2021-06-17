using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public  class MusicStore : MonoBehaviour
{
    public  new AudioSource audio;
    public  AudioClip[] playlist;

    public void Update()
    {
        if (StatData.isPlayable)
        {
            audio.clip = playlist[StatData.musicTest];
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
    }
}