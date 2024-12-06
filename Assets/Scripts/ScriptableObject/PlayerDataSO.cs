using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Player Data SO")]
public class PlayerDataSO : ScriptableObject
{
    public string playerName = "Newbie";
    public int playerType = 0;
    public int playerWave = 0;
    public int playerStage = 0;

    public int playerHP = 0;
    public int playerNuke = 0;
    public int playerScore = 0;
    public int playerBulletLevel = 0;
    public string playerGunType = "MainGun";

    public bool playerPlaying = false;

    public PlayerDataSO playerDataDefaultSO;

    public void ResetDataToDefault(){
        playerType = playerDataDefaultSO.playerType;
        playerWave = playerDataDefaultSO.playerWave;
        playerStage = playerDataDefaultSO.playerStage;
        playerHP = playerDataDefaultSO.playerHP;
        playerNuke = playerDataDefaultSO.playerNuke;
        playerScore = playerDataDefaultSO.playerScore;
        playerBulletLevel = playerDataDefaultSO.playerBulletLevel;
        playerName = playerDataDefaultSO.playerName;
        playerGunType = "MainGun";
    }

}
