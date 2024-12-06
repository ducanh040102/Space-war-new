using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunPool : PlayerGunPool
{
    float Angle;

    public override void FireBullet()
    {
        if (GameManager.instance.GetBulletLevel() == 0)
        {
            GameObject bullet1 = SpawnObject(spawnPosition.position);
            bullet1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return;
        }
        if (GameManager.instance.GetBulletLevel() == 1)
        {
            GameObject bullet1 = SpawnObject(spawnPosition.position + new Vector3(.4f, 0, 0));
            GameObject bullet2 = SpawnObject(spawnPosition.position + new Vector3(-.4f, 0, 0));

            bullet1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bullet2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            return;
        }
        
        if (GameManager.instance.GetBulletLevel() == 2)
        {
            GameObject bullet1 = SpawnObject(spawnPosition.position + new Vector3(.4f, 0, 0));
            GameObject bullet2 = SpawnObject(spawnPosition.position + new Vector3(-.4f, 0, 0));
            GameObject bullet3 = SpawnObject(spawnPosition.position);

            bullet1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bullet2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bullet3.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            return;
        }

        if (GameManager.instance.GetBulletLevel() > 2)
        {
            Angle = -15f;
            Vector3 vec = new Vector3(0, 0, Angle);
            for (int i = 0; i < GameManager.instance.GetBulletLevel(); i++)
            {
                GameObject bullet = SpawnObject(spawnPosition.position);
                bullet.transform.rotation = Quaternion.Euler(vec);
                vec.z += 2 * Mathf.Abs(Angle) / (GameManager.instance.GetBulletLevel() - 1);
            }
        }

    }
}
