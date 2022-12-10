using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseMenu : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        // Initialize the pause menu
        // You can add your own code here to set up the pause menu

    }

    // Update is called once per frame
    void Update() {
        // Check if the player has pressed the pause button
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // If the game is currently running, pause it
            if (Time.timeScale == 1) {
                PauseGame();
            }
            // If the game is currently paused, resume it
            else {
                ResumeGame();
            }
        }
    }

    // This function is called when the player wants to pause the game
    void PauseGame() {
        // Set the timeScale to 0, which will pause the game
        Time.timeScale = 0;
    }

    // This function is called when the player wants to resume the game
    void ResumeGame() {
        // Set the timeScale back to 1, which will resume the game
        Time.timeScale = 1;
    }

    // This function is called when the player wants to close the game
    void QuitGame() {
        // Save any game data here

        // Quit the application
        Application.Quit();
    }
}