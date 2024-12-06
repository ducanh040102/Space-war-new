using System;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public event EventHandler OnBossSpawn;
    public event EventHandler<OnBossDamagedEventArgs> OnBossDamaged;
    public event EventHandler OnBossDestroy;

    public class OnBossDamagedEventArgs : EventArgs
    {
        public float _bossMaxHP;
        public float _bossHP;
    }

    [SerializeField] private float bossHP;
    [SerializeField] private float bossMaxHP;

    private void Awake()
    {
        instance = this;
    }

    public void BossSpawn()
    {
        OnBossSpawn?.Invoke(this, EventArgs.Empty);
        bossMaxHP = bossHP;
    }

    public void BossDestroy()
    {
        OnBossDestroy?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateBossHp(float hp)
    {
        if (hp < bossHP)
        {
            OnBossDamaged?.Invoke(this, new OnBossDamagedEventArgs{
                _bossMaxHP = bossMaxHP,
                _bossHP = hp
            });
        }
        bossHP = hp;
    }

    public void BossSpawnHelper(string helperDataName)
    {
        LoadCSV.instance.LoadNewCSV(helperDataName);
        EnemySpawner.Instance.SpawnEnemy();
    }
}
