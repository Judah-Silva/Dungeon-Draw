using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    public SceneRouter GetSceneRouter()
    {
        return GetComponent<SceneRouter>();
    }

    public LevelTracker GetLevelTracker()
    {
        return GetComponent<LevelTracker>();
    }

    public PlayerStats GetPlayerStats()
    {
        return GetComponent<PlayerStats>();
    }
}
