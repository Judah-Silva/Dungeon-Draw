using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    public string battleScene = "BattleScene";
    public string shopScene = "Shop";
    public string restScene = "Rest";
    public string eventScene = "Event";

    public MapManager mapManager;

    public Color disabledColor = Color.gray;
    private bool wasClicked = false;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (wasClicked)
        {
            Debug.Log("already visited");
            return;
        }
        
        if (gameObject.CompareTag("battle"))
        {
            Debug.Log("battle engaged");
            // LevelTracker.levelsVisited++;
            SceneManager.LoadScene(battleScene);
        }
        else if (gameObject.CompareTag("shop"))
        {
            Debug.Log("shop entered");
            // LevelTracker.levelsVisited++;
            SceneManager.LoadScene(shopScene);
        }
        else if (gameObject.CompareTag("rest"))
        {
            Debug.Log("resting");
            // LevelTracker.levelsVisited++;
            SceneManager.LoadScene(restScene);
        }
        else if (gameObject.CompareTag("event"))
        {
            Debug.Log("event triggered");
            // LevelTracker.levelsVisited++;
            SceneManager.LoadScene(eventScene);
        } 
        else if (gameObject.CompareTag("boss"))
        {
            Debug.Log("loading boss...");
            return; // to prevent boss icon from being disabled (probably temporary)
        }
        else
        {
            Debug.Log("this icon doesn't exist");
        }

        LevelTracker.IncrementLevelsVisited();
        DisableIcons(); // probably not necessary anymore but idk
    }

    private void DisableIcons()
    {
        wasClicked = true;
        rend.material.color = disabledColor;
        
        float checkDist = 5f;

        // check for icons above
        Ray rayUp = new Ray(transform.position, transform.forward);
        RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, checkDist))
        {
            IconManager hitIcon = hitUp.collider.GetComponent<IconManager>();
            if (hitIcon != null && !hitIcon.wasClicked)
            {
                hitIcon.DisableIcons();
            }
        }

        // check for icons below
        Ray rayDown = new Ray(transform.position, transform.forward * -1f);
        RaycastHit hitDown;
        if(Physics.Raycast(rayDown, out hitDown, checkDist))
        {
            IconManager hitIcon = hitDown.collider.GetComponent<IconManager>();
            if (hitIcon != null && !hitIcon.wasClicked)
            {
                hitIcon.DisableIcons();
            }
        }
    }
}
