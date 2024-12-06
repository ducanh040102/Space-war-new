using System;
using UnityEngine;

public enum TypeOfGun
{
    MainGun,
    DoubleshotGun,
    LaserGun,
    HomingGun,
}

public class PlayerBulletManager : MonoBehaviour
{
    public static PlayerBulletManager instance;

    public TypeOfGun typeBullet = TypeOfGun.MainGun;
    public EventHandler OnPLayerShoot;

    [SerializeField] private float mainGunAttackInterval;
    [SerializeField] private float doubleshotGunAttackInterval;
    [SerializeField] private MainGunPool mainGunPool;
    [SerializeField] private DoubleshotPool doubleshotPool;
    [SerializeField] private PlayerHomingBulletPool homingPool;
    [SerializeField] private NukePool nukePool;
    [SerializeField] private GameObject laserPrefab;

    private GameObject pLaser;
    private Laser laser;
    private Respawn respawn;

    private TypeOfGun previousTypeBullet;

    private float attackInterval;
    private float attackIntervalCountdown;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Enum.TryParse<TypeOfGun>(GameManager.instance.GetPlayerGunType(), out typeBullet);

        previousTypeBullet = typeBullet;
        attackIntervalCountdown = 0;

        respawn = gameObject.GetComponent<Respawn>();

        pLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser = pLaser.GetComponent<Laser>();

        GameplayUI.instance.OnFireNukeButton += OnFireNukeButton;
    }

    private void OnFireNukeButton(object sender, System.EventArgs e){
        FireNuke();
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;
        
        Fire();
        ChangeBulletType();

        if (attackIntervalCountdown > 0)
        {
            attackIntervalCountdown -= Time.deltaTime;
        }
    }

    private void ChangeBulletType()
    {
        if (typeBullet != previousTypeBullet)
        {
            laser.DisableLaser();
            previousTypeBullet = typeBullet;
        }

        if (typeBullet == TypeOfGun.MainGun)
        {
            attackInterval = mainGunAttackInterval;
        }

        else if (typeBullet == TypeOfGun.DoubleshotGun)
        {
            attackInterval = doubleshotGunAttackInterval;
        }
        
        else if (typeBullet == TypeOfGun.HomingGun)
        {
            attackInterval = doubleshotGunAttackInterval;
        }
    }

    private void Fire()
    {
        if (respawn.isInvisible)
        {
            laser.DisableLaser();
            return;
        }

        TapMouse();
        HoldMouse();
        UnholdMouse();
    }

    private void TapMouse(){
        if (Input.GetMouseButtonDown(0))
        {
            if (typeBullet == TypeOfGun.LaserGun)
            {
                laser.EnableLaser();
                AudioManager.instance.PlayPlayerShootLaser();
            }

            if (typeBullet == TypeOfGun.MainGun)
                mainGunPool.FireBullet();
            if (typeBullet == TypeOfGun.DoubleshotGun)
                doubleshotPool.FireBullet();
            if (typeBullet == TypeOfGun.HomingGun)
                homingPool.FireBullet();
        }
    }

    private void HoldMouse(){
        if (Input.GetMouseButton(0))
        {
            if (typeBullet == TypeOfGun.LaserGun)
            {
                laser.UpdateLaser(transform, Vector3.up);
            }

            else
            {
                if (attackIntervalCountdown <= 0)
                {
                    if (typeBullet == TypeOfGun.DoubleshotGun)
                        doubleshotPool.FireBullet();
                    if (typeBullet == TypeOfGun.MainGun)
                        mainGunPool.FireBullet();
                    if (typeBullet == TypeOfGun.HomingGun)
                        homingPool.FireBullet();

                    AudioManager.instance.PlayPlayerShoot();

                    attackIntervalCountdown = attackInterval;
                }
            }
        }
    }

    private void UnholdMouse(){
        if (Input.GetMouseButtonUp(0))
        {
            laser.DisableLaser();
        }
    }

    private void FireNuke()
    {
        if (GameManager.instance.GetNukeValue() > 0)
        {
            nukePool.FireBullet();

            AudioManager.instance.PlayPlayerNuke();
            GameManager.instance.DecreaseNuke();
        }
    }
}
