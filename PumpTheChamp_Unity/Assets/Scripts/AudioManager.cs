using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioObject
{
    public string Name;
    public AudioClip Clip;
}

[Serializable]
public class AudioTrack
{
    public string Name;
    public AudioType AudioType;
    public AudioSource Source;
    public List<AudioObject> Audio;
}

public enum AudioType
{
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    public List<AudioTrack> Tracks;

    public List<AudioClip> musicClips;
    public List<AudioClip> SFXClips;

    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource sfxSource;

    //[Header("Background Music")]
    //[SerializeField] AudioSource backgroundMusic;
    //public AudioSource BackgroundMusic { get => backgroundMusic; }

    // Static reference to the instance
    private static AudioManager instance;

    // Property to access the instance
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
        else
        {
            // Set the instance
            instance = this;

            // Optional: Prevent the Singleton object from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAudio(AudioSource audio)
    {
        if (audio.clip != null)
        {
            audio.Play();
        }
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = musicClips.Find(c => c.name == name);

        if (clip == null)
        {
            Debug.Log($"Sound Not Found");
        }

        else
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        AudioClip clip = SFXClips.Find(c => c.name == name);

        if (clip == null)
        {
            Debug.Log($"Sound Not Found");
        }

        else
        {
            sfxSource.clip = clip;
            sfxSource.Play();
        }
    }
}
