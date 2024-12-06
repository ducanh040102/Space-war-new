using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public bool isInvisible;


    [SerializeField] private Transform playerName;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private float respawnCountdown;

    private void Start()
    {
        Player.instance.OnPlayerHit += Player_OnPlayerHit;
    }

    private void Player_OnPlayerHit(object sender, System.EventArgs e)
    {
        if (GameManager.instance.GetHitPointsValue() <= 1)
        {
            GameOver();
            return;
        }
        
        StartCoroutine(WaitForRespawn(respawnCountdown));
    }

    private IEnumerator WaitForRespawn(float time)
    {
        PlayerInvisible();
        yield return new WaitForSeconds(time);
        Player.instance.PowerupShield(3f);
        PlayerVisible();
    }

    private void GameOver()
    {
        PlayerInvisible();
        StartCoroutine(WaitForEndGame());
    }

    private void PlayerVisible()
    {
        playerName.gameObject.SetActive(true);
        capsuleCollider.enabled = true;
        playerVisual.Show();
        isInvisible = false;
    }
    
    private void PlayerInvisible()
    {
        playerName.gameObject.SetActive(false);
        capsuleCollider.enabled = false;
        playerVisual.Hide();
        isInvisible = true;
    }

    IEnumerator WaitForEndGame()
    {
        GameplayUI.instance.StageTextTrigger("GAME OVER", "");
        yield return new WaitForSeconds(5f);
        GameManager.instance.GameEnded();
    }
}
