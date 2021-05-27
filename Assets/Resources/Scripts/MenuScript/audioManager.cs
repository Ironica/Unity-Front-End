using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = playlist[0];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
