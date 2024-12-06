using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float baseHitPoint = 50;
    [SerializeField] private float hitPoint;
    [SerializeField] private int scoreValue;
    [SerializeField] private bool isStartAction = false;

    protected EnemyBulletSpawner enemyBulletSpawner;
    private PowerupSpawner powerupSpawner;

    public bool IsStartAction { get => isStartAction; private set => isStartAction = value; }
    public float HitPoint { get => hitPoint; private set => hitPoint = value; }
    public float BaseHitPoint { get => baseHitPoint; private set => baseHitPoint = value; }

    private void Start()
    {
        InitialStats();
        StartCoroutine(WaitForAttack());
    }

    private void Update()
    {
        Fire();
        Destroy();
    }

    protected virtual void InitialStats()
    {
        powerupSpawner = PowerupSpawner.sharedInstance;
        enemyBulletSpawner = gameObject.GetComponent<EnemyBulletSpawner>();

        HitPoint = BaseHitPoint * (GameManager.instance.GetPlayerStage() + 1);
    }

    protected virtual void Fire()
    {
        enemyBulletSpawner.SpawnBullet();
    }

    public void Hit(float damage)
    {
        if (IsStartAction)
        {
            HitPoint -= damage;
        }
    }

    protected virtual void Destroy()
    {
        if(HitPoint <= 0)
        {
            transform.DOKill();
            powerupSpawner.SpawnPowerup(transform);
            enemyBulletSpawner.StopFiring();

            EnemySpawner.Instance.enemySpawnedList.Remove(transform);
            VFXManager.instance.SpawnExplosion(transform.position, Vector3.one, 1);
            AudioManager.instance.PlayExplode();
            GameManager.instance.UpdateScore(scoreValue);

            Destroy(gameObject);
        }
        
    }

    protected virtual IEnumerator WaitForAttack()
    {
        while (!IsStartAction)
        {
            yield return null;
        }
        enemyBulletSpawner.StartFiring();
    }

    public void StartAction()
    {
        IsStartAction = true;
    }
}
