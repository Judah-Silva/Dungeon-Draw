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
    
    public int columns = 3;
    public static int MaxColumns;
    public static int ColIndex;
    
    public static float iconSpacing = 5f;

    private IconManager _iconManager;

    private void Start()
    {
        _iconManager = IconManager.Instance;
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

            if (r >= 1 && r <= 45)
            {
                _iconManager.CreateIcon(0, spawnPos);
            }
            else if (r >= 46 && r <= 70)
            {
                _iconManager.CreateIcon(1, spawnPos);
            }
            else if (r >= 71 && r <= 81)
            {
                _iconManager.CreateIcon(2, spawnPos);
            }
            else if (r >= 82 && r <= 100)
            {
                _iconManager.CreateIcon(3, spawnPos);
            }
        }
    }

    public void SpawnBoss(Vector3 startPos)
    {
        Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, startPos.z);
        _iconManager.CreateIcon(4, spawnPos);
    }

    public void SpawnDisabled(Vector3 startPos)
    {
        float columnSpacing = 4f;

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
                _iconManager.CreateIcon(5, spawnPos);
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
