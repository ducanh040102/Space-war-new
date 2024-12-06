using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float perSec;
    [SerializeField] private float maxLength;
    [SerializeField] private float baseWidth;

    [SerializeField] private Vector3 direction;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform startVFX;
    [SerializeField] private Transform endVFX;

    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float countdown;
    [SerializeField] private bool isPlayerLaser;
   
    private void Start()
    {
        DisableLaser();
    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            countdown -= Time.deltaTime;
        }
    }

    public void EnableLaser()
    {
        countdown = perSec;
        lineRenderer.enabled = true;
        startVFX.gameObject.SetActive(true);
        endVFX.gameObject.SetActive(true);

        if(isPlayerLaser)
        {
            float laserWidth = baseWidth + (GameManager.instance.GetBulletLevel() / 10f);
            lineRenderer.startWidth = laserWidth;
            lineRenderer.endWidth = laserWidth;
        }
    }


    public void UpdateLaser(Transform firingObject, Vector3 offset)
    {
        if (firingObject == null)
            DisableLaser();

        Vector3 spawnPosition = firingObject.position + offset;

        RaycastHit2D hit = Physics2D.Raycast(spawnPosition, direction.normalized, maxLength, obstacleLayer);

        Vector3 hitPosition = hit ? new Vector3(spawnPosition.x, hit.point.y + .1f) : spawnPosition + direction.normalized * maxLength;

        
        if (hit.collider != null)
        {
            if(countdown <= 0)
            {
                if (isPlayerLaser)
                {
                    float finalDamage = baseDamage + (GameManager.instance.GetBulletLevel() * 15f);
                    hit.transform.GetComponent<Enemy>().Hit(finalDamage);
                }

                else
                {
                    Player.instance.Damage();
                }
                countdown = perSec;
            } 
        }
        
        lineRenderer.SetPosition(0, spawnPosition);
        lineRenderer.SetPosition(1, hitPosition);

        startVFX.position = spawnPosition;
        endVFX.position = hitPosition;
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
        startVFX.gameObject.SetActive(false);
        endVFX.gameObject.SetActive(false);
    }
}
