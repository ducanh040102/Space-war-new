using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused;

    private void Start() {
        GameplayUI.instance.OnPauseButton += OnPauseButton;
    }

    private void OnPauseButton(object sender, System.EventArgs e){
        PauseGame();
    }

    public void PauseGame()
    {
        pauseMenuUI.GetComponent<PauseMenuUI>().Show();
        Time.timeScale = 0f;
        AudioManager.instance.PlaySFX(AudioManager.instance.pause);
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.GetComponent<PauseMenuUI>().Hide();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu() { 
        Time.timeScale = 1f;
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}

