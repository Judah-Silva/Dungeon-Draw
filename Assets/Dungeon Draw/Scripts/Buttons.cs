using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string mapScene = "MapSelect";
    public SceneTransition sceneTransition;
    
    public void Map()
    { 
        Debug.Log("Map button clicked");
        sceneTransition.FadeToScene(mapScene);
    }

    public void Deck()
    {
        Debug.Log("Deck button clicked");
    }

    public void Settings()
    {
        Debug.Log("Settings button clicked");
    }

}
