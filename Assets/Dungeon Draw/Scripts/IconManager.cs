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

    public MapManager mapManager;

    private bool wasClicked = false;
    
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
            mapManager.SpawnIcons(gameObject.transform.position);
            // SceneManager.LoadScene(battleScene);
        }
        else if (gameObject.CompareTag("shop"))
        {
            Debug.Log("shop entered");
            mapManager.SpawnIcons(gameObject.transform.position);
            // SceneManager.LoadScene(shopScene);
        }
        else if (gameObject.CompareTag("rest"))
        {
            Debug.Log("resting");
            mapManager.SpawnIcons(gameObject.transform.position);
            // SceneManager.LoadScene(restScene);
        }
        else if (gameObject.CompareTag("event"))
        {
            Debug.Log("event triggered");
            mapManager.SpawnIcons(gameObject.transform.position);
            // SceneManager.LoadScene(eventScene);
        }
        else
        {
            Debug.Log("this icon doesn't exist");
        }

        DisableIcons();
    }

    private void DisableIcons()
    {
        wasClicked = true;
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
