using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : Spawner
{
    //[SerializeField] Sprite[] debrisSprites;

    //private float debrisSpawnCountdown;

    //private void Update()
    //{
    //    debrisSpawnCountdown -= Time.deltaTime;

    //    if (debrisSpawnCountdown <= 0)
    //    {
    //        Vector3 debrisSpawnPosition = new Vector3(Random.Range(-ScreenBoundary.Instance.ScreenWidth, ScreenBoundary.Instance.ScreenWidth), gameObject.transform.position.y);
    //        Transform debrisSpawed = Spawn(debrisSpawnPosition);

    //        debrisSpawed.TryGetComponent<Debris>(out Debris debris);
    //        if (debris != null)
    //        {
    //            int randomDebrisSpritesIndex = Random.Range(0, debrisSprites.Length);
    //            Sprite debrisSprite = debrisSprites[randomDebrisSpritesIndex];
    //            debris.SetDebrisSprite(debrisSprite);
    //        }

    //        debrisSpawnCountdown = GetSpawnRandomCountdown();
    //    }
        
        
    //}
}

