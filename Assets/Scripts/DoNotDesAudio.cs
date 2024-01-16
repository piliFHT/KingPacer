using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class DoNotDesAudio : MonoBehaviour
{
    private static bool isInstanceCreated = false;
    private static AudioSource audioSource; // Reference to the audio source

    public AudioClip SampleSceneMusic; // Music for SampleScene
    public AudioClip LevelOneMusic; // Music for LevelOne

    void Awake()
    {
        if (isInstanceCreated)
        {
            Destroy(gameObject);
            return;
        }

        isInstanceCreated = true;
        // DontDestroyOnLoad(transform.gameObject); // Comment out this line

        // Get the audio source and assign it to the static variable
        audioSource = GetComponent<AudioSource>();

        // Add a listener to the sceneLoaded event
    }

    void Update()
    {
        // Check the current scene and play the appropriate music using the static reference
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (audioSource.clip != SampleSceneMusic)
            {
                audioSource.clip = SampleSceneMusic; // Set the audio clip
                audioSource.Play(); // Play the audio
            }
        }
        else if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            if (audioSource.clip != LevelOneMusic)
            {
                audioSource.clip = LevelOneMusic; // Set the audio clip
                audioSource.Play(); // Play the audio
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    
}
