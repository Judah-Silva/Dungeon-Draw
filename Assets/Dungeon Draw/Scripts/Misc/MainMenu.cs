using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    

    private SceneRouter sceneRouter;

    private void Start()
    {
        sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    public void StartGame()
    {
        // MapManager.CurrentColumns++;
        sceneRouter.ToMap();
    }

    public void Credits()
    {
        sceneRouter.ToCredits();
    }

    public void QuitGame()
    {
        // TODO: make this actually quit the game
        Application.Quit();
        Debug.Log("quitting game");
    }
}
