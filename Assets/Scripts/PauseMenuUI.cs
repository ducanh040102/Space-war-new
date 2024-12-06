using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button toMenuButton;
    [SerializeField] private GameObject container;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SoundSettingUI soundSettingUI;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            pauseMenu.ResumeGame();
        });

        settingButton.onClick.AddListener(() =>
        {
            soundSettingUI.Show();
            Hide();
        });

        toMenuButton.onClick.AddListener(() =>
        {
            pauseMenu.ResumeGame();
            GameManager.instance.ExitToMenu();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        container.SetActive(true);
    }

    public void Hide()
    {
        container.SetActive(false);
    }
}
