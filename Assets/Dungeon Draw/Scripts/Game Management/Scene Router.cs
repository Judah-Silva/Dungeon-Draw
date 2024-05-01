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
