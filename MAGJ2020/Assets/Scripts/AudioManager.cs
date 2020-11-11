using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Array of all the game's sounds and musics
    public Sound[] sounds;

    public static AudioManager instance;    // Reference to the current instance of the manager to keep it a singleton

    // Called before start : creates an AudioSource for each of the game's sounds
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);            // Destroy the duplicate AudioManager when changing scene
            return;
        }

        DontDestroyOnLoad(gameObject);      // Prevents the music/sfx from cutting when changing scene by keeping the object

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Play the sound of the matching name
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found in the AudioManager");
            return;
        }
        s.source.Play();
    } 
}
