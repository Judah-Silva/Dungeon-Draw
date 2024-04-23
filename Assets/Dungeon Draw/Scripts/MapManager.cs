using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public string mainMenuScene = "MainMenu";

    public Transform startPoint;
    public GameObject battleIcon;
    public GameObject shopIcon;
    public GameObject restIcon;
    public GameObject eventIcon;
    
    public static float iconSpacing = 5f;

    private void Start()
    {
        Vector3 startPos = startPoint.position;
        SpawnIcons(startPos);
    }

    public void SpawnIcons(Vector3 startPos)
    {
        float columnSpacing = 5f;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                columnSpacing = 4f;
            }
            if (i == 1)
            {
                columnSpacing = -1f;
            }
            if (i == 2)
            {
                columnSpacing = -6f;
            }
            
            Vector3 spawnPos = new Vector3(startPos.x + iconSpacing, startPos.y, columnSpacing);
            int r = Random.Range(1, 6);

            if (r == 1)
            {
                GameObject newIcon = Instantiate(battleIcon, spawnPos, Quaternion.identity);
            }
            else if (r == 2)
            {
                GameObject newIcon = Instantiate(shopIcon, spawnPos, Quaternion.identity);
            }
            else if (r == 3)
            {
                GameObject newIcon = Instantiate(restIcon, spawnPos, Quaternion.identity);
            }
            else if (r == 4)
            {
                GameObject newIcon = Instantiate(eventIcon, spawnPos, Quaternion.identity);
            }
            else if (r == 5)
            {
                GameObject newIcon = Instantiate(battleIcon, spawnPos, Quaternion.identity);
            }
            else if (r == 6)
            {
                GameObject newIcon = Instantiate(battleIcon, spawnPos, Quaternion.identity);
            }

            // columnSpacing -= 5f;
        }
        
        
        
    }

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
