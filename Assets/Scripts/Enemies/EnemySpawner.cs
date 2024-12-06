using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public event EventHandler<OnEnterNewWaveEventArgs> OnEnterNewWave;
    public class OnEnterNewWaveEventArgs : EventArgs
    {
        public string _wave;
        public string _text;
    }


    public List<Transform> enemySpawnedList;

    [SerializeField] private LoadCSV loadCSV;
    [SerializeField] private Transform[] enemyPrefab;
    [SerializeField] private Transform[] bossPrefab;
    [SerializeField] private Transform spawnPosittion;
    [SerializeField] private Transform endInitialPostition;
    [SerializeField] private float spawnDelayMax;
    [SerializeField] private bool bossWave = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(NextWave());

        BossManager.instance.OnBossSpawn += OnBossSpawn;
        BossManager.instance.OnBossDestroy += OnBossDestroy;
    }

    private void OnBossSpawn(object sender, EventArgs e)
    {
        bossWave = true;
    }

    private void OnBossDestroy(object sender, EventArgs e)
    {
        HitAllEnemy(10000f);

        StartCoroutine(NextStage());
    }

    private IEnumerator NextWave()
    {
        string[] data = loadCSV.LoadNewCSV("Wave" + GameManager.instance.GetPlayerwave());

        if (data != null)
        {
            string[] row = loadCSV.ReadSpawnRow(6);

            OnEnterNewWave?.Invoke(this, new OnEnterNewWaveEventArgs
            {
                _wave = GameManager.instance.GetPlayerwave().ToString(),
                _text = row[0]
            });

            yield return new WaitForSeconds(5);

            SpawnEnemy();
            while (enemySpawnedList.Count != 0)
            {
                yield return null;
            }

            GameManager.instance.IncreaseWave();

            while (bossWave == true)
            {
                yield return null;
            }

            StartCoroutine(NextWave());
        }

        if (data == null)
        {
            StartCoroutine(EndGame());
        }

        yield return null;
    }

    IEnumerator NextStage()
    {
        GameManager.instance.IncreaseStage();
        yield return new WaitForSeconds(30f);
        bossWave = false;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(10f);
        GameManager.instance.GameEnded();
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < 6; i++)
        {
            string[] row = loadCSV.ReadSpawnRow(i);
            for (int j = 0; j < 9; j++)
            {
                Vector3 position = new Vector3(j * 2f + endInitialPostition.position.x, i + endInitialPostition.position.y, 0);

                if (int.Parse(row[j]) == 1)
                {
                    SpawnEnemy(enemyPrefab[0], position);
                }

                if (int.Parse(row[j]) == 2)
                {
                    SpawnEnemy(enemyPrefab[1], position);
                }

                if (int.Parse(row[j]) == 3)
                {
                    SpawnEnemy(enemyPrefab[2], position);
                }

                if (int.Parse(row[j]) == 11)
                {
                    SpawnEnemy(bossPrefab[0], position);
                }
            }
        }
    }

    private Transform SpawnEnemy(Transform enemyPrefab, Vector3 position)
    {
        Transform enemySpawned = Instantiate(enemyPrefab, spawnPosittion.position, enemyPrefab.transform.rotation);
        enemySpawnedList.Add(enemySpawned);
        enemySpawned.DOMove(position, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            enemySpawned.GetComponent<Enemy>().StartAction();
        });

        return enemySpawned;
    }

    public void HitAllEnemy(float damage)
    {
        if (enemySpawnedList.Count == 0)
            return;
        foreach (Transform enemy in enemySpawnedList)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().Hit(damage);
            }
        }
    }
}
