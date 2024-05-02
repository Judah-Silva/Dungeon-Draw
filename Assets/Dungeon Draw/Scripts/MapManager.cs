using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public Transform startPoint;
    public GameObject battleIcon;
    public GameObject shopIcon;
    public GameObject restIcon;
    public GameObject eventIcon;
    public GameObject bossIcon;
    public GameObject disabledIcon;
    
    public int columns = 3;
    public static int MaxColumns;
    public static int ColIndex;
    
    public static float iconSpacing = 5f;

    private void Start()
    {
        MaxColumns = columns;
        ColIndex = LevelTracker.levelsVisited;
        Vector3 startPos = startPoint.position;
        
        Debug.Log("colindex " + ColIndex);
        Debug.Log("levelsvisited " + LevelTracker.levelsVisited);
        
        if (ColIndex == 0)
        {
            SpawnIcons(startPos);
        }
        else
        {
            SpawnDisabled(startPos);
        }
    }

    public void SpawnIcons(Vector3 startPos)
    {
        if (ColIndex == MaxColumns)
        {
            SpawnBoss(startPos);
            return;
        }
        
        float columnSpacing = 5f;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                columnSpacing = 4f;
            }
            if (i == 1)
            {
                columnSpacing = -1f;
            }
            if (i == 2)
            {
                columnSpacing = -6f;
            }
            
            Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, columnSpacing);
            int r = Random.Range(1, 100);

            if (r >= 1 && r <= 65)
            {
                GameObject newIcon = Instantiate(battleIcon, spawnPos, Quaternion.identity);
            }
            else if (r >= 66 && r <= 79)
            {
                GameObject newIcon = Instantiate(shopIcon, spawnPos, Quaternion.identity);
            }
            else if (r >= 80 && r <= 90)
            {
                GameObject newIcon = Instantiate(restIcon, spawnPos, Quaternion.identity);
            }
            else if (r >= 91 && r <= 100)
            {
                GameObject newIcon = Instantiate(eventIcon, spawnPos, Quaternion.identity);
            }
        }
    }

    public void SpawnBoss(Vector3 startPos)
    {
        Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, startPos.z);
        GameObject newIcon = Instantiate(bossIcon, spawnPos, Quaternion.identity);
    }

    public void SpawnDisabled(Vector3 startPos)
    {
        float columnSpacing = 5f;

        for (int j = 0; j < ColIndex; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    columnSpacing = 4f;
                }
                if (i == 1)
                {
                    columnSpacing = -1f;
                }
                if (i == 2)
                {
                    columnSpacing = -6f;
                }
            
                Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, columnSpacing);
                GameObject newIcon = Instantiate(disabledIcon, spawnPos, Quaternion.identity);
            }

            startPos = new Vector3(startPos.x + iconSpacing, startPos.y, startPos.z);
        }
        
        SpawnIcons(startPos);
    }
}
