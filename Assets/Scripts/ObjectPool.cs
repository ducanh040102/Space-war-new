using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject poolPrefab;
    public int poolSize;

    private List<GameObject> pool;

    public Transform spawnPosition;

    private void Awake()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        if (pool == null)
        {
            pool = new List<GameObject>();
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject poolObject = Instantiate(poolPrefab);
            poolObject.transform.position = spawnPosition.position;
            poolObject.SetActive(false);
            pool.Add(poolObject);
        }
    }

    public GameObject SpawnObject(Vector3 location)
    {
        foreach (GameObject pObject in pool)
        {
            if (!pObject.activeSelf)
            {
                pObject.SetActive(true);
                pObject.transform.position = location;
                return pObject;
            }
        }

        GameObject poolObject = Instantiate(poolPrefab);
        poolObject.transform.position = location;
        poolObject.SetActive(true);
        pool.Add(poolObject);

        return poolObject;
    }
}
