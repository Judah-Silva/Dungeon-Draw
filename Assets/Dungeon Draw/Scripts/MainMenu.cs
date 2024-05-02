using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string mapScene = "MapSelect";
    public string creditsScene = "Credits";
    public string mainMenuScene = "MainMenu";

    public SceneTransition transition;

    public AudioSource src;
    public AudioClip buttonSelectSound;
    public AudioClip quitSound;
    
    // starting a new game
    public void StartGame()
    {
        PlaySFX(buttonSelectSound);
        LevelTracker.levelsVisited = 0;
        LevelTracker.floorsVisited = 1;
        // TODO: overwrite totalLevels and floors when starting new game?
        transition.FadeToScene(mapScene);
    }

    public void ContinueGame()
    {
        PlaySFX(buttonSelectSound);
        
        int totalLevels = PlayerPrefs.GetInt("TotalLevels", 0);
        LevelTracker.levelsVisited = totalLevels;
        
        int totalFloors = PlayerPrefs.GetInt("TotalFloors", 0);
        LevelTracker.floorsVisited = totalFloors;
        
        transition.FadeToScene(mapScene);
    }

    public void Credits()
    {
        PlaySFX(buttonSelectSound);
        SceneManager.LoadScene(creditsScene);
    }

    public void ReturnToMenu()
    {
        PlaySFX(quitSound);
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        PlaySFX(quitSound);
        
        // TODO: make this actually quit the game
        Debug.Log("quitting game");
    }

    public void PlaySFX(AudioClip sfx)
    {
        src = gameObject.GetComponent<AudioSource>();
        src.clip = sfx;
        src.Play();
    }
}
