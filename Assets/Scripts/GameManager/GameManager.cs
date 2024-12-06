using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private int maxHitPoints;
    [SerializeField] private PlayerDataSO playerDataSO;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Player.instance.OnPlayerHit += Player_OnPlayerHit;
    }

    private void Player_OnPlayerHit(object sender, System.EventArgs e)
    {
        DecreaseHitPoints();
        DecreBulletLevel();
    }

    public void UpdateScore(int points)
    {
        playerDataSO.playerScore += points;
    }

    public void IncreaseHitPoints()
    {
        playerDataSO.playerHP++;
    }

    public void DecreaseHitPoints()
    {
        playerDataSO.playerHP--;
        if (playerDataSO.playerHP < 0)
            playerDataSO.playerHP = 0;
    }

    public void IncreaseNuke()
    {
        playerDataSO.playerNuke++;
    }

    public void DecreaseNuke()
    {
        playerDataSO.playerNuke--;
    }

    public void IncreaseWave()
    {
        playerDataSO.playerWave++;
    }
    public void IncreaseStage()
    {
        playerDataSO.playerStage++;
    }

    public void IncreaseBulletLevel()
    {
        if (GetBulletLevel() >= 8) return;
        playerDataSO.playerBulletLevel++;
    }
    
    public void DecreBulletLevel()
    {
        if (GetBulletLevel() <= 0) return;
        playerDataSO.playerBulletLevel--;
    }

    public void SetGunType(TypeOfGun typeOfGun){
        playerDataSO.playerGunType = typeOfGun.ToString();
    }

    public int GetHitPointsValue()
    {
        return playerDataSO.playerHP;
    }

    public int GetNukeValue()
    {
        return playerDataSO.playerNuke;
    }

    public int GetScoreValue()
    {
        return playerDataSO.playerScore;
    }
    
    public string GetPlayerNameValue()
    {
        return playerDataSO.playerName;
    }

    public int GetBulletLevel()
    {
        return playerDataSO.playerBulletLevel;
    }
    public int GetPlayerType()
    {
        return playerDataSO.playerType;
    }
    public int GetPlayerwave()
    {
        return playerDataSO.playerWave;
    }
    
    public int GetPlayerStage()
    {
        return playerDataSO.playerStage;
    }

    public string GetPlayerGunType(){
        return playerDataSO.playerGunType;
    }

    public void ExitToMenu()
    {
        playerDataSO.playerPlaying = false;
        if(playerDataSO.playerWave <= 1)
            playerDataSO.ResetDataToDefault();
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    public void GameEnded()
    {
        Loader.Load(Loader.Scene.HighscoreTableScene);
    }
}
