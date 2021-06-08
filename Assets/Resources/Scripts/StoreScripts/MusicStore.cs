using UnityEngine;
using UnityEngine.Audio;

public class MusicStore : MonoBehaviour
{
    public AudioSource audio;

    public void SetVolume()
    {
        if (audio.volume != 0f)
        {
            audio.volume = 0f;
            audio.Stop();
        }
        else
        {
            audio.volume = 0.2f;
            audio.Play();
        }
          
    }
}