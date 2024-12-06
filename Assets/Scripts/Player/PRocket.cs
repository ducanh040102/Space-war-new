using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timer;


    private void OnEnable() {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        EnemySpawner.Instance.HitAllEnemy(damage);
        VFXManager.instance.SpawnExplosion(transform.position, Vector3.one * 5, 0.5f);
        AudioManager.instance.PlayBigExplode();
        gameObject.SetActive(false);
    }
}
