using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mapScene = "MapSelect";
    public string creditsScene = "Credits";
    public string mainMenuScene = "MainMenu";
    
    public void StartGame()
    {
        // MapManager.CurrentColumns++;
        SceneManager.LoadScene(mapScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        // TODO: make this actually quit the game
        Debug.Log("quitting game");
    }
}
