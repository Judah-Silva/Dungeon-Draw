using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mapScene = "MapSelect";
    public string mainMenu = "MainMenu";
    
    public void StartGame()
    {
        SceneManager.LoadScene(mapScene);
    }

    public void QuitGame()
    {
        // TODO: make this actually quit the game
        Debug.Log("quitting game");
    }
}
