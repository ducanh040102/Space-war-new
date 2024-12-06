using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum TypeOfPowerup
    {
        Heath,
        Shield,
        Nuke,
        MainGun,
        DoubleshotGun,
        LaserGun,
        HomingGun,
    }

    [SerializeField] private TypeOfPowerup typeOfPowerup;
    [SerializeField] private float speedFall = .5f;
    [SerializeField] private Vector3 direction = Vector3.down;

    private void Start()
    {
        Destroy(gameObject, 15f);
    }

    private void Update()
    {
        Move();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                ActPowerup();
                Destroy(gameObject);
            }
        }
    }

    void Move()
    {
        transform.localPosition += direction * speedFall * Time.deltaTime;
    }

    private void ActPowerup()
    {
        GameManager.instance.UpdateScore(12000);

        if (typeOfPowerup == TypeOfPowerup.Heath || typeOfPowerup == TypeOfPowerup.Nuke || typeOfPowerup == TypeOfPowerup.Shield)
        {
            ItemPowerup();
        }

        else
        {
            GunPowerup();
        }
    }

    private void ItemPowerup()
    {
        switch (typeOfPowerup)
        {
            case TypeOfPowerup.Shield:
                Player.instance.PowerupShield(3f);
                break;
            case TypeOfPowerup.Heath:
                GameManager.instance.IncreaseHitPoints();
                break;
            case TypeOfPowerup.Nuke:
                GameManager.instance.IncreaseNuke();
                break;
        }

        AudioManager.instance.PlayItemPick();
    }

    private void GunPowerup()
    {
        Enum.TryParse<TypeOfGun>(typeOfPowerup.ToString(), out TypeOfGun typeOfGun);

        if (PlayerBulletManager.instance.typeBullet != typeOfGun){
            PlayerBulletManager.instance.typeBullet = typeOfGun;
            GameManager.instance.SetGunType(typeOfGun);
        }
            
        else
            GameManager.instance.IncreaseBulletLevel();
        AudioManager.instance.PlayWeaponUpgrade();
    }
}
