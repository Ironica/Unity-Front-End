using UnityEngine;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public new AudioSource audio;

    void Start()
    {
        audio.clip = playlist[StatData.musicUsed];
        audio.Play();
    }
    
    void Update()
    {
        if (!audio.isPlaying && audio.volume != 0f)
        {
            audio.clip = playlist[StatData.musicUsed];
            audio.Play();
        }
    }
    
    public void SetVolume()
    {
        if (audio.volume != 0f)
        {
            audio.volume = 0f;
            audio.Pause();
        }
        else
        {
            audio.volume = 0.5f;
            audio.Play();
        }
          
    }
}
