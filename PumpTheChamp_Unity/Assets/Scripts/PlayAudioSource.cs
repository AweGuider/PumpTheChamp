using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        if (source == null) source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }
    }
}
