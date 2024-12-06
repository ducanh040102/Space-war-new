using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Hit(damage);
                BackToPool();
            }
        }
    }

    void Update()
    {
        Move();
        CrossBoarderBackToPool();
    }

    protected virtual void Move()
    {
        transform.Translate(transform.up * Time.deltaTime * moveSpeed);
    }

    
}
