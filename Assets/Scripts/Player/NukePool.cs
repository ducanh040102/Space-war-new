using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePool : PlayerGunPool
{

    public override void FireBullet()
    {
        SpawnObject(spawnPosition.position);
    }
}
