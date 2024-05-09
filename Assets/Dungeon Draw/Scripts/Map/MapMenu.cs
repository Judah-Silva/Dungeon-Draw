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
        // save gold
        PlayerPrefs.SetInt("Gold", PlayerStats.Coins);
        
        // save deck 
        List<int> savedDeck = PlayerStats.Deck;
        
        PlayerPrefs.SetInt("SavedDeckCount", savedDeck.Count);
        for (int i = 0; i < savedDeck.Count; i++)
        {
            PlayerPrefs.SetInt("SavedDeck" + i, savedDeck[i]);
        }
        PlayerPrefs.Save();
        
        sceneRouter.ToMainMenu();
    }

    public void OptionsMenu()
    {
        inventoryUI.SetActive(true);
    }
}
