using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public Transform startPoint;

    // 0: battle, 1: shop, 2: rest, 3: event, 4: boss, 5: disabled
    public GameObject icon;
    
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
                GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
                newIcon.GetComponent<Icon>().init(0);
            }
            else if (r >= 66 && r <= 79)
            {
                GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
                newIcon.GetComponent<Icon>().init(1);
            }
            else if (r >= 80 && r <= 90)
            {
                GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
                newIcon.GetComponent<Icon>().init(2);
            }
            else if (r >= 91 && r <= 100)
            {
                GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
                newIcon.GetComponent<Icon>().init(3);
            }
        }
    }

    public void SpawnBoss(Vector3 startPos)
    {
        Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, startPos.z);
        GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
        newIcon.GetComponent<Icon>().init(4);
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
                GameObject newIcon = Instantiate(icon, spawnPos, Quaternion.identity);
                newIcon.GetComponent<Icon>().init(5);
            }

            startPos = new Vector3(startPos.x + iconSpacing, startPos.y, startPos.z);
        }
        
        SpawnIcons(startPos);
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.GetSceneRouter().ToMainMenu();
    }

    public void OptionsMenu()
    {
        // TODO: open player inventory menu
        Debug.Log("options menu");
    }
}
