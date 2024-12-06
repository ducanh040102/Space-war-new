using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public static PowerupSpawner sharedInstance = null;

    [SerializeField] private GameObject[] powerups;
    [SerializeField] private float[] spawnWeights;
    [SerializeField] float powerupDropChance = .1f;

   
    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    public void SpawnPowerup(Transform position)
    {
        if (Random.value < powerupDropChance)
        {
            GameObject powerup = ChooseRandomPowerUp();

            Instantiate(powerup, position.position, Quaternion.identity);
        }
    }


    private GameObject ChooseRandomPowerUp()
    {
        float totalWeight = 0f;

        foreach (float weight in spawnWeights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < powerups.Length; i++)
        {
            cumulativeWeight += spawnWeights[i];
            if (randomValue <= cumulativeWeight)
            {
                return powerups[i];
            }
        }

        return powerups[powerups.Length - 1];
    }
}
