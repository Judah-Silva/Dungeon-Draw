using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public static int levelsVisited = 0;
    public static int floorsVisited = 1;

    private int totalLevels;
    private string totalLevelsKey = "TotalLevels";
    private int totalFloors;
    private string totalFloorsKey = "TotalFloors";
    

    private void Start()
    {
        totalLevels = PlayerPrefs.GetInt(totalLevelsKey, 0);
        totalFloors = PlayerPrefs.GetInt(totalFloorsKey, 0);
    }

    public void IncrementLevelsVisited()
    {
        levelsVisited++;
        CheckLevels();
    }

    public void CheckLevels()
    {
        if (levelsVisited != totalLevels)
        {
            totalLevels = levelsVisited;
            PlayerPrefs.SetInt(totalLevelsKey, totalLevels);
            PlayerPrefs.Save();
        }
    }
    
    public void IncrementFloorsVisited()
    {
        floorsVisited++;
        CheckFloors();
    }

    public void CheckFloors()
    {
        // != so it gets overwritten on each new playthrough
        if (floorsVisited != totalFloors)
        {
            totalFloors = floorsVisited;
            PlayerPrefs.SetInt(totalFloorsKey, totalFloors);
            PlayerPrefs.Save();
        }
    }

    public void ResetLevels()
    {
        // sets to -1 bc levelsVisited is incremented again at end of IconManager
        levelsVisited = -1;
        
        totalLevels = levelsVisited;
        PlayerPrefs.SetInt(totalLevelsKey, totalLevels);
        PlayerPrefs.Save();
    }

    // to update floors visited when starting new game
    public void ResetFloors()
    {
        totalFloors = floorsVisited;
        PlayerPrefs.SetInt(totalFloorsKey, totalFloors);
        PlayerPrefs.Save();
    }
}
