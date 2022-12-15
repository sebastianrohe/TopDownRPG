using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    // The audio source that will play the music
    public AudioSource audioSource;

    // The UI button that will be used to start and stop the music
    public Button musicButton;

    void Start()
    {
        // Get a reference to the button game object
        musicButton = GameObject.Find("MusicButton").GetComponent<Button>();

        // Add a listener to the button's onClick event
        musicButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleMusic()
    {
        // If the audio source is currently playing, pause it
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        // Otherwise, play the audio source
        else
        {
            audioSource.Play();
        }
    }
}