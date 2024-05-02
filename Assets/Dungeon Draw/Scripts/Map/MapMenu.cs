using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public string mainMenuScene = "MainMenu";
    public SceneRouter sceneRouter;
    public GameObject inventoryUI;
    public TextMeshProUGUI floorText;
    
    // Start is called before the first frame update
    void Start()
    {

        sceneRouter = GameManager.Instance.GetSceneRouter();
        floorText.text = "Floor " + LevelTracker.floorsVisited;
    }
    
    public void ReturnToMainMenu()
    {
        sceneRouter.ToMainMenu();
    }

    public void OptionsMenu()
    {
        inventoryUI.SetActive(true);
    }
}
