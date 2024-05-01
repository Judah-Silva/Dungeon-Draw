using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public static int levelsVisited = 0;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void IncrementLevelsVisited()
    {
        levelsVisited++;
    }
}
