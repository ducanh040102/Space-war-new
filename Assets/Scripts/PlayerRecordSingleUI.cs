using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRecordSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerWaveText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    
    public void SetRecord(string name, string wave, string score)
    {
        playerNameText.text = name;
        playerWaveText.text = wave;
        playerScoreText.text = score;
    }

}
