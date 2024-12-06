using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHomingBullet : PlayerBullet
{
    private Transform enemyTransform;

    void Update()
    {
        if(enemyTransform == null){
            enemyTransform = FindNearestEnemy();
        }
        
        if(enemyTransform != null){
            Move();
        }
        else{
            transform.position += Vector3.up * (moveSpeed/2) * Time.deltaTime;
        }

        CrossBoarderBackToPool();
    }

    private Transform FindNearestEnemy(){

        try{
            float shortestDistance = 1000f;
            Transform closestEnemy = null;
            Vector3 pos = transform.position;

        foreach (Transform enemy in EnemySpawner.Instance.enemySpawnedList)
        {
            Vector3 direction = enemy.position - pos;
            float distance = direction.magnitude;
            if(distance < shortestDistance){
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
        }
        catch{
            return null;
        }
    }


    override protected void Move(){
        transform.position = Vector3.MoveTowards(transform.position, enemyTransform.position, moveSpeed * Time.deltaTime);
        Vector3 offset = transform.position - enemyTransform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
    }
}
