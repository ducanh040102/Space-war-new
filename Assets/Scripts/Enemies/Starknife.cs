using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starknife : MonoBehaviour
{
    private EnemyBulletSpawner enemyBulletSpawner;

    private void Start()
    {
        enemyBulletSpawner = gameObject.GetComponent<EnemyBulletSpawner>();
    }

    private void LookAtPlayer()
    {
        if (Player.instance != null)
        {
            Vector3 direction = Player.instance.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward * 5 * Time.deltaTime);
            transform.rotation = rotation;
        }
    }
    void Update()
    {
        LookAtPlayer();
    }
}
