using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public string mainMenuScene = "MainMenu";

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OptionsMenu()
    {
        // TODO: open player inventory menu
        Debug.Log("options menu");
    }
}
