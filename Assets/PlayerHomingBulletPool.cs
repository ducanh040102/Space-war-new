using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingBulletPool : PlayerGunPool
{
    public override void FireBullet()
    {
        if (GameManager.instance.GetBulletLevel() >= 0)
        {
            SpawnObject(spawnPosition.position);
        }
        
    }

    
}
