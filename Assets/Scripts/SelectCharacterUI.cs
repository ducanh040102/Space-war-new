using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSO;
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextCharacterButton;
    [SerializeField] private Button prevCharacterButton;
    [SerializeField] private Image characterView;
    [SerializeField] private TextMeshProUGUI nameBoxText;
    [SerializeField] private Sprite[] characterSprite;

    private int selectedPlayerType = 0;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Play();
        });

        nextCharacterButton.onClick.AddListener(() =>
        {
            NextCharacter();
        });

        prevCharacterButton.onClick.AddListener(() =>
        {
            PreviousCharacter();
        });
    }

    private void Start()
    {
        selectedPlayerType = 0;
    }

    private void Play()
    {
        AssignNewPlayerData(nameBoxText.text, selectedPlayerType);

        if (nameBoxText.text != "")
            StartCoroutine(LoadGameplayScene());
    }

    private void NextCharacter()
    {
        if (selectedPlayerType == characterSprite.Length - 1)
            selectedPlayerType = 0;
        else
            selectedPlayerType++;
        characterView.sprite = characterSprite[selectedPlayerType];
    }

    private void PreviousCharacter()
    {
        if (selectedPlayerType == 0)
            selectedPlayerType = characterSprite.Length - 1;
        else
            selectedPlayerType--;

        characterView.sprite = characterSprite[selectedPlayerType];
    }

    private void AssignNewPlayerData(string name, int type)
    {
        playerDataSO.ResetDataToDefault();

        playerDataSO.playerPlaying = true;
        playerDataSO.playerName = name;
        playerDataSO.playerType = type;
    }

    IEnumerator LoadGameplayScene()
    {
        yield return new WaitForEndOfFrame();
        Loader.Load(Loader.Scene.GameplayScene);
    }
}
