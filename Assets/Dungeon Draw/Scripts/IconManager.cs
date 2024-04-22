using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    public string battleScene = "BattleScene";
    public string shopScene = "ShopScene";
    public string restScene = "RestSpotScene";
    public string eventScene = "EventScene";
    
    private void OnMouseDown()
    {
        if (gameObject.CompareTag("battle"))
        {
            SceneManager.LoadScene(battleScene);
        }
        else if (gameObject.CompareTag("shop"))
        {
            SceneManager.LoadScene(shopScene);
        }
        else if (gameObject.CompareTag("rest"))
        {
            SceneManager.LoadScene(restScene);
        }
        else if (gameObject.CompareTag("event"))
        {
            SceneManager.LoadScene(eventScene);
        }
        else
        {
            Debug.Log("this icon doesn't exist");
        }
    }
}
