using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider hpSlider;

    public SpriteRenderer visual;

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

    private IEnumerator DisableVisualHurtAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        visual.color = Color.white;
    }

    protected virtual void InitialStats()
    {
        powerupSpawner = PowerupSpawner.sharedInstance;
        enemyBulletSpawner = gameObject.GetComponent<EnemyBulletSpawner>();

        if (hpSlider != null)
            hpSlider.value = 1;

        HitPoint = BaseHitPoint * (GameManager.instance.GetPlayerStage() + 1);
        BaseHitPoint = HitPoint;
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
            if(hpSlider != null)
                hpSlider.value = hitPoint / baseHitPoint;

            visual.color = Color.red;
            StartCoroutine(DisableVisualHurtAfterDelay());
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
