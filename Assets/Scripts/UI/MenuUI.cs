using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSO;

    [SerializeField] private Button newgameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button highscoreButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SoundSettingUI soundsetting;
    [SerializeField] private AudioSource uiSoundEffect;

    private void Awake()
    {
        if (playerDataSO.playerWave > 1 && playerDataSO.playerHP > 0)
            continueButton.gameObject.SetActive(true);

        newgameButton.onClick.AddListener(() =>
        {
            uiSoundEffect.Play();
            Loader.Load(Loader.Scene.DialogueScene);
        });
        
        continueButton.onClick.AddListener(() =>
        {
            uiSoundEffect.Play();
            playerDataSO.playerPlaying = true;
            Loader.Load(Loader.Scene.GameplayScene);
        });

        settingsButton.onClick.AddListener(() =>
        {
            uiSoundEffect.Play();
            soundsetting.Show();
        });

        highscoreButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.HighscoreTableScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            uiSoundEffect.Play();
            Application.Quit();
        });

        
    }
    

}
