using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoubleshotPool : PlayerGunPool
{
    [SerializeField] private ObjectPool doubleshotUpgradePool;

    public override void FireBullet()
    {

        if (GameManager.instance.GetBulletLevel() == 0)
        {
            SpawnObject(spawnPosition.position);
            
        }
        if (GameManager.instance.GetBulletLevel() == 1)
        {
            SpawnObject(spawnPosition.position + new Vector3(.2f, 0, 0));
            SpawnObject(spawnPosition.position + new Vector3(-.2f, 0, 0));
        }
        if (GameManager.instance.GetBulletLevel() == 2)
        {
            SpawnObject(spawnPosition.position + new Vector3(.3f, 0, 0));
            SpawnObject(spawnPosition.position + new Vector3(-.3f, 0, 0));
            SpawnObject(spawnPosition.position + new Vector3(0, .1f, 0));
        }

        if (GameManager.instance.GetBulletLevel() == 3)
        {
            SpawnObject(spawnPosition.position + new Vector3(.3f, 0, 0));
            SpawnObject(spawnPosition.position + new Vector3(-.3f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(0, .1f, 0));
        }

        if (GameManager.instance.GetBulletLevel() == 4)
        {
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.2f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.2f, 0, 0));
        }
        
        if (GameManager.instance.GetBulletLevel() == 5)
        {
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.3f, .1f, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.3f, .1f, 0));
            SpawnObject(spawnPosition.position);
        }
        
        if (GameManager.instance.GetBulletLevel() == 6)
        {
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.3f, .0f, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.3f, .0f, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(0, .1f, 0));
        }

        if(GameManager.instance.GetBulletLevel() == 7)
        {
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.3f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.3f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(0, .1f, 0));
            SpawnObject(spawnPosition.position + new Vector3(.6f, -.1f, 0));
            SpawnObject(spawnPosition.position + new Vector3(-.6f, -.1f, 0));
        }
        
        if(GameManager.instance.GetBulletLevel() == 8)
        {
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.3f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.3f, 0, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(0, .1f, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(.5f, -.1f, 0));
            doubleshotUpgradePool.SpawnObject(spawnPosition.position + new Vector3(-.5f, -.1f, 0));
        }
        
    }
}
