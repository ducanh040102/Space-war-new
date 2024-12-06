using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    public static GameplayUI instance;

    [SerializeField] private Transform fadeIn;
    [SerializeField] private Transform fadeOut;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private TextMeshProUGUI bossHealthPercent;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI hitPointText;
    [SerializeField] private TextMeshProUGUI nukeText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button fireNukeButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private float fadeInDuration = 10f;
    [SerializeField] private float fadeOutDuration = 10f;

    public EventHandler OnFireNukeButton;
    public EventHandler OnPauseButton;


    private void Awake()
    {
        instance = this;

        fireNukeButton.onClick.AddListener(()=>{
            OnFireNukeButton?.Invoke(this, EventArgs.Empty);
        });

        pauseButton.onClick.AddListener(()=>{
            OnPauseButton?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start()
    {
        EnemySpawner.Instance.OnEnterNewWave += EnemySpawner_OnEnterNewWave;
        BossManager.instance.OnBossSpawn += BossManager_OnBossSpawn;
        BossManager.instance.OnBossDamaged += BossManager_OnBossDamaged;
        BossManager.instance.OnBossDestroy += BossManager_OnBossDestroy;

        bossHealthBar.gameObject.SetActive(false);

        StartCoroutine(SceneFadeOut(0));
    }

    private void BossManager_OnBossDamaged(object sender, BossManager.OnBossDamagedEventArgs e)
    {
        float percent = e._bossHP / e._bossMaxHP;
        bossHealthBar.value = e._bossHP/e._bossMaxHP;
        bossHealthPercent.text = String.Format("{0:0.00}", (percent * 100f)) + "%";
    }

    private void BossManager_OnBossSpawn(object sender, System.EventArgs e)
    {
        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.value = 1;
        bossHealthPercent.text = "100%";
    }

    private void BossManager_OnBossDestroy(object sender, System.EventArgs e)
    {
        if(bossHealthBar != null)
        {
            bossHealthPercent.text = "0%";
            bossHealthBar.gameObject.SetActive(false);
        }
        
        StageTextTrigger("Stage Clear", "Prepare To Warp");
    }

    private void EnemySpawner_OnEnterNewWave(object sender, EnemySpawner.OnEnterNewWaveEventArgs e)
    {
        StageTextTrigger("Wave " + e._wave, e._text);
    }

    public void StageTextTrigger(string top, string bottom)
    {
        stageNameText.text = top + "\n" + bottom;
        stageNameText.gameObject.SetActive(true);
          
        StartCoroutine(WaitForStageText());
    }

    IEnumerator WaitForStageText()
    {
        yield return new WaitForSeconds(3);
        stageNameText.gameObject.SetActive(false);
    }

    private void DisplayedInformation()
    {
        hitPointText.text = GameManager.instance.GetHitPointsValue().ToString();
        nukeText.text = GameManager.instance.GetNukeValue().ToString();
        scoreText.text = GameManager.instance.GetScoreValue().ToString();
        powerText.text = GameManager.instance.GetBulletLevel().ToString();
    }

    void Update()
    {
        DisplayedInformation();
    }

    IEnumerator SceneFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        fadeIn.gameObject.SetActive(true);
        fadeOut.gameObject.SetActive(false);
        yield return new WaitForSeconds(fadeInDuration);
        fadeIn.gameObject.SetActive(false);

    }
    
    IEnumerator SceneFadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        fadeIn.gameObject.SetActive(false);
        fadeOut.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeOutDuration);
        fadeOut.gameObject.SetActive(false);
    }
    
    public void SceneFadeInOut(float fadeInDelay = 3f)
    {
        StartCoroutine(SceneFadeIn(fadeInDelay));
        StartCoroutine(SceneFadeOut(fadeInDelay + fadeInDuration));
    }

    
}
