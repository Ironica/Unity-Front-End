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
            audio.clip = playlist[StatData.indexStoreMusic];
            audio.volume = 0.2f;
            audio.Play();
        }
        else
        {
            audio.Stop();
        }
    }
}