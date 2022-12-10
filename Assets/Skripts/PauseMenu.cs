using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu; // the pause menu game object 
    public Button resumeButton; // the resume button
    public Button quitButton; // the quit button
    public Button pauseButton; // the quit button

    // Use this for initialization
    void Start() {
        // add listeners to the buttons
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        quitButton.onClick.AddListener(PauseGame);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

    }

    public void QuitGame() {
        Application.Quit();
    }
}