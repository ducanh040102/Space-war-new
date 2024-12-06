using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSO;
    [SerializeField] private Transform entryContent;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Button backToMenuButton;

    private List<Transform> highscoreTableEntryTransformList;

    private void Awake()
    {
        backToMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        entryTemplate.gameObject.SetActive(false);
        if (playerDataSO.playerPlaying)
        {
            AddHighscoreTableEntry(playerDataSO.playerScore, playerDataSO.playerName, playerDataSO.playerWave);
        }

        playerDataSO.playerPlaying = false;
    }

    private void Start()
    {

        LoadHighscoreTable();
    }

    private void CreateHighscoreEntry(HighscoreTableEntry highscoreTableEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("RankText").GetComponent<TextMeshProUGUI>().text = rank.ToString();
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = highscoreTableEntry.name;
        entryTransform.Find("WaveText").GetComponent<TextMeshProUGUI>().text = highscoreTableEntry.wave.ToString();
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = highscoreTableEntry.score.ToString();

        transformList.Add(entryTransform);
    }

    private void AddHighscoreTableEntry(int _score, string _name, int _wave)
    {
        HighscoreTableEntry highscoreTableEntry = new HighscoreTableEntry() { name = _name, score = _score, wave = _wave };

        Highscore highscore;
        string jsonString = PlayerPrefs.GetString("highscoreTable","");

        if (jsonString != "")
        {
            highscore = JsonUtility.FromJson<Highscore>(jsonString);
            highscore.highscoreTableEntryList.Add(highscoreTableEntry);
        }
        else{
            List<HighscoreTableEntry> newHighscoreTableEntryList = new List<HighscoreTableEntry>() { highscoreTableEntry };
            highscore = new Highscore() {highscoreTableEntryList = newHighscoreTableEntryList};
        }
            
        string json = JsonUtility.ToJson(highscore);
        
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        playerDataSO.ResetDataToDefault();
    }

    private void LoadHighscoreTable()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscore highscore = JsonUtility.FromJson<Highscore>(jsonString);

        if(highscore == null) return;
        for (int i = 0; i < highscore.highscoreTableEntryList.Count; i++)
        {
            for (int j = 0; j < highscore.highscoreTableEntryList.Count; j++)
            {
                if (highscore.highscoreTableEntryList[i].score > highscore.highscoreTableEntryList[j].score)
                {
                    HighscoreTableEntry highscoreTableEntryTmp = highscore.highscoreTableEntryList[i];
                    highscore.highscoreTableEntryList[i] = highscore.highscoreTableEntryList[j];
                    highscore.highscoreTableEntryList[j] = highscoreTableEntryTmp;
                }
            }
        }

        highscoreTableEntryTransformList = new List<Transform>();
        foreach (HighscoreTableEntry highscoreTableEntry in highscore.highscoreTableEntryList)
        {
            CreateHighscoreEntry(highscoreTableEntry, entryContent, highscoreTableEntryTransformList);
        }

    }

    [System.Serializable]
    private class Highscore
    {
        public List<HighscoreTableEntry> highscoreTableEntryList;
    }

    [System.Serializable]
    private class HighscoreTableEntry
    {
        public string name;
        public int wave;
        public int score;
    }
}


