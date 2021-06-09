using UnityEngine;

public class MusicStore : MonoBehaviour
{
    public new AudioSource audio;
    public AudioClip[] playlist;

    public void SetVolume()
    {
        audio.clip = playlist[1];
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