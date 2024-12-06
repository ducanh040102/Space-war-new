using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecordUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform playerRecordTemplate;
    [SerializeField] private PlayerDataSO playerDataSO;


    private void Start()
    {
        ReadData();
    }

    private void InsertData()
    {

    }

    private void ReadData()
    {
        string[] data = LoadCSV.instance.LoadNewCSV("leaderboard");

        if (data != null)
        {
            foreach (Transform child in container)
            {
                if (child == playerRecordTemplate)
                    continue;
                Destroy(child.gameObject);
            }


            for (int i = 0; i < data.Length - 1; i++)
            {
                string[] row = LoadCSV.instance.ReadSpawnRow(i);
                if(row == null) { return; }

                Transform recipeTransform = Instantiate(playerRecordTemplate, container);
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.GetComponent<PlayerRecordSingleUI>().SetRecord(row[0], row[1], row[2]);
            }       
        }
    }
}
