using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    public SceneRouter sceneRouter;
    
    public static IconManager Instance { get; private set; }

    private void Awake()
    {
        sceneRouter = GameManager.Instance.GetSceneRouter();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Route(int type)
    {
        switch (type)
        {
            case 0:
                sceneRouter.ToBattle(/* maybe add param here to differentiate types of battles */);
                break;
            case 1:
                sceneRouter.ToShop();
                break;
            case 2:
                sceneRouter.ToRest();
                break;
            case 3:
                sceneRouter.ToEvent();
                break;
            case 4:
                sceneRouter.ToBattle();
                break;
                
        }

        LevelTracker.IncrementLevelsVisited();
    }
}
