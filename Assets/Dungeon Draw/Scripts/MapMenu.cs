using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public string mainMenuScene = "MainMenu";
    public SceneTransition transition;
    public GameObject inventoryUI;
    public TextMeshProUGUI floorText;
    
    // Start is called before the first frame update
    void Start()
    {
        floorText.text = "Floor " + LevelTracker.floorsVisited;
    }
    
    public void ReturnToMainMenu()
    {
        transition.FadeToScene(mainMenuScene);
    }

    public void OptionsMenu()
    {
        inventoryUI.SetActive(true);
    }
}
