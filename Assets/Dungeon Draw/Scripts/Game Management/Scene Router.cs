using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRouter : MonoBehaviour
{
    public SceneFader sceneFader;
    
    [Header("Routes")]
    public string mapScene = "MapSelect";
    public string creditsScene = "Credits";
    public string mainMenuScene = "MainMenu";
    public string battleScene = "Battle Scene";
    public string shopScene = "Shop";
    public string restScene = "Rest";
    public string eventScene = "Event";
    public string bossScene = "Boss Scene";
    public string gameOverScene = "GameOver";
    
    public void ToMap()
    {
        sceneFader.FadeTo(mapScene);
    }

    public void ToCredits()
    {
        sceneFader.FadeTo(creditsScene);
    }

    public void ToMainMenu()
    {
        sceneFader.FadeTo(mainMenuScene);
    }

    public void ToBattle()
    {
        sceneFader.FadeTo(battleScene);
        // add logic here to get the battle set up
    }

    public void ToShop()
    {
        sceneFader.FadeTo(shopScene);
    }

    public void ToRest()
    {
        sceneFader.FadeTo(restScene);
    }

    public void ToEvent()
    {
        sceneFader.FadeTo(eventScene);
    }

    public void ToBoss()
    {
        sceneFader.FadeTo(bossScene);
    }

    public void ToGameOver()
    {
        sceneFader.FadeTo(gameOverScene);
    }

    public void ToDeck()
    {
        Debug.Log("Deck button clicked");
    }

    public void ToSettings()
    {
        Debug.Log("Settings button clicked");
    }

}
