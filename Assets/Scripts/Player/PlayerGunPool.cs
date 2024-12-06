using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGunPool : ObjectPool
{
    private void Awake()
    {
        CreatePool();
    }
    public abstract void FireBullet();
}
