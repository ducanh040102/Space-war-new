using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    [SerializeField] private ObjectPool explosionPool;
    [SerializeField] private Transform background;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player.instance.OnPlayerHit += Player_OnPlayerHit;
        BossManager.instance.OnBossDestroy += BossManager_OnBossDestroy;
    }

    private void Player_OnPlayerHit(object sender, System.EventArgs e)
    {
        SpawnExplosion(Player.instance.transform.position, Vector3.one);
    }

    private void BossManager_OnBossDestroy(object sender, System.EventArgs e){
        SpawnExplosion(BossManager.instance.transform.position, Vector3.one * 100f);
    }

    public void SpawnExplosion(Vector3 position, Vector3 size, float playbackSpeed = 1f)
    {
        GameObject explosion = explosionPool.SpawnObject(position);
        ParticleSystem explosionParticleSystem = explosion.GetComponent<ParticleSystem>();
        var ps = explosionParticleSystem.main;
        ps.loop = false;
        ps.simulationSpeed = playbackSpeed;

        explosion.transform.localScale = size;
    }
}
