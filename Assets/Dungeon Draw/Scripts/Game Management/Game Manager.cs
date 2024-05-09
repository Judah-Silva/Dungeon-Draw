using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

    [HideInInspector]
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
        private set => _instance = value;
    }


    private void Awake()
    {
        if (_instance == null)
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

    public MusicManager GetMusicManager()
    {
        return GetComponent<MusicManager>();
    }
}
