using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField] private Button skipButton;

    private void Awake()
    {
        skipButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
}
