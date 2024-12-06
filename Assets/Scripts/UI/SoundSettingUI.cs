using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Transform container;
    [SerializeField] private Button backButton;

    private void Awake() {
        backButton.onClick.AddListener(() =>{
            if(pauseMenu != null)
                pauseMenu.ResumeGame();
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        container.gameObject.SetActive(true);
    }

    public void Hide()
    {
        container.gameObject.SetActive(false);
    }
}

