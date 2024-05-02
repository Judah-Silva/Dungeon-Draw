using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ParticleSystem = UnityEngine.ParticleSystem;

public class IconManager : MonoBehaviour
{
    public string newScene = "";

    public MapManager mapManager;
    public LevelTracker levelTracker;

    public Color mainColor;
    public Color disabledColor = Color.gray;
    public Color hoverColor = Color.white;
    
    private bool wasClicked = false;
    private Renderer rend;

    private ParticleSystem particles;

    // public ParticleSystem particles;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        mainColor = rend.material.color;
        particles = GetComponent<ParticleSystem>();
        // DontDestroyOnLoad(gameObject);
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = mainColor;
    }

    private void OnMouseDown()
    {
        if (wasClicked)
        {
            Debug.Log("already visited");
            return;
        }
        
        particles.Play();
        
        SceneManager.LoadScene(newScene);

        levelTracker.IncrementLevelsVisited();
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
