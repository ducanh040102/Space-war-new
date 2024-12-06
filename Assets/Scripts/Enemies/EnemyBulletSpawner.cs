using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBulletSpawner : Spawner
{
    public enum BulletPool
    {
        ClawBossBulletPool,
        StarEnemyBulletPool,
        StarknifeEnemyBulletPool,
        SlicerEnemyBulletPool,
    }

    [SerializeField] private bool isFiring = false;

    [SerializeField] private Transform enemyFiringPoint;

    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private FiringPattern firingPattern;
    
    [SerializeField] private int laserDuration;

    private float enemyFireCountdown = 0;
    private Transform laserBullet;
    private Laser laser;

    public bool IsFiring { get => isFiring; private set => isFiring = value; }

    public enum FiringPattern
    {
        StraightDownPattern,
        ThreeShotPattern,
        StarPattern,
        HomingPattern,
        StraightDownLaser
    }

    private void Start()
    {
        objectPool = GameObject.Find(bulletPool.ToString()).GetComponent<ObjectPool>();
        enemyFireCountdown = GetSpawnRandomCountdown();
    }

    private void Update()
    {
        if(IsFiring)
        {
            enemyFireCountdown -= Time.deltaTime;
        }
    }

    public void SpawnBullet()
    {
        if (IsFiring)
        {
            if (enemyFireCountdown <= 0)
            {
                switch (firingPattern)
                {
                    case FiringPattern.StraightDownPattern:
                        StraightDown();
                        break;
                    case FiringPattern.ThreeShotPattern:
                        ThreeShot();
                        break;
                    case FiringPattern.HomingPattern:
                        Homing();
                        break;
                    case FiringPattern.StraightDownLaser:
                        Laser();
                        break;
                }
            }
        }
    }

    private void StraightDown()
    {
        Transform bullet = Spawn(enemyFiringPoint.position);

        bullet.GetComponent<ObjectMoveInScene>().UpdateMoveType(ObjectMoveInScene.Move.Down);
        enemyFireCountdown = GetSpawnRandomCountdown();
    }

    private void ThreeShot()
    {
        Transform middleBullet = Spawn(enemyFiringPoint.position);
        Transform leftBullet = Spawn(enemyFiringPoint.position + Vector3.left);
        Transform rightBullet = Spawn(enemyFiringPoint.position + Vector3.right);


        middleBullet.GetComponent<ObjectMoveInScene>().UpdateMoveType(ObjectMoveInScene.Move.Down);
        leftBullet.GetComponent<ObjectMoveInScene>().UpdateMoveType(ObjectMoveInScene.Move.DownLeft);
        rightBullet.GetComponent<ObjectMoveInScene>().UpdateMoveType(ObjectMoveInScene.Move.DownRight);

        enemyFireCountdown = GetSpawnRandomCountdown();
    }
    
    private void Homing()
    {
        Transform bullet = Spawn(enemyFiringPoint.position);
        ObjectMoveInScene objectMoveInScene = bullet.GetComponent<ObjectMoveInScene>();
        objectMoveInScene.SetTarget(Player.instance.transform);
        objectMoveInScene.UpdateMoveType(ObjectMoveInScene.Move.FlyToTarget);

        enemyFireCountdown = GetSpawnRandomCountdown();
    }

    private void Laser()
    {
        if(laserBullet == null)
        {
            laserBullet = Spawn(enemyFiringPoint.position);
        }
            
        
        if(laserBullet != null)
        {
            if (transform == null)
                return;
            laser = laserBullet.GetComponent<Laser>();
            laser.EnableLaser();
            laser.UpdateLaser(transform, Vector3.down);
            StartCoroutine(WaitForDisableLaser(laser));
            
        }    
    }

    public void StartFiring()
    {
        IsFiring = true;
    }

    public void StopFiring()
    {
        IsFiring = false;
    }

    private IEnumerator WaitForDisableLaser(Laser laser)
    {
        yield return new WaitForSeconds(laserDuration);
        laser.DisableLaser();
        enemyFireCountdown = GetSpawnRandomCountdown();
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;
        if (laser == null)
            return;
        laser.DisableLaser();
        laser.gameObject.SetActive(false);
    }
}
