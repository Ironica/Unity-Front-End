using UnityEngine;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public new AudioSource audio; 
    private Random rand = new Random();
    private int musicIndex;
    
    void Start()
    {
        
        musicIndex = rand.Next(3);
        audio.clip = playlist[musicIndex];
        audio.Play();
    }
    
    void Update()
    {
        if (!audio.isPlaying)
        {
            musicIndex = rand.Next(3);
            audio.clip = playlist[musicIndex];
            audio.Play();
        }
    }
}
