using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private LoopingBackground loopingBackground;

    private void Start() {
        loopingBackground.ScrollSpeedEase(100f,5f, 5f);
        loopingBackground.ChangeBG(GameManager.instance.GetPlayerStage());

        BossManager.instance.OnBossDestroy += BossManager_OnBossDestroy; 
    }

    private void BossManager_OnBossDestroy(object sender, System.EventArgs e){
        StartCoroutine(SwitchStageSequence());
    }


    IEnumerator SwitchStageSequence(){
        AudioManager.instance.PlayBigExplode();
        AudioManager.instance.PlayVictoryTheme(5f);
        yield return new WaitForSeconds(3f);
        loopingBackground.ScrollSpeedEase(5f,100f, 10f);
        AudioManager.instance.PlayPlayerWarpIn();
        yield return new WaitForSeconds(15f);
        GameplayUI.instance.SceneFadeInOut(0);
        yield return new WaitForSeconds(2f);
        loopingBackground.ChangeBG(GameManager.instance.GetPlayerStage());
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlayPlayerWarpOut();
        loopingBackground.ScrollSpeedEase(100f,5f, 10f);
        yield return new WaitForSeconds(10f);
        AudioManager.instance.PlayBGM(GameManager.instance.GetPlayerStage());

    }
}
