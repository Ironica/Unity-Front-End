using UnityEngine;
using UnityEngine.Audio;

public class GestionMusic : MonoBehaviour
{
    public new AudioSource audio;

   public void SetVolume()
      {
          if (audio.volume != 0f)
          {
              audio.volume = 0f;
             audio.Pause();
          }
          else
          {
              audio.volume = 0.2f;
             audio.Play();
          }
          
      }
}
