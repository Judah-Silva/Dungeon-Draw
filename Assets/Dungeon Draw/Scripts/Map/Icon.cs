using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public int type;
    public Color hoverColor = Color.white;
    
    private Color _mainColor;
    private IconManager _iconManager;
    private Renderer _renderer;
    
    private ParticleSystem particles;

    private void Start()
    {
        if (type == 5)
        {
            enabled = false;
            return;
        }
        
        _iconManager = IconManager.Instance;
        _renderer = GetComponent<Renderer>();
        _mainColor = _renderer.material.color;
        particles = GetComponent<ParticleSystem>();
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _mainColor;
    }
    
    private void OnMouseDown()
    {
        particles.Play();
        _iconManager.Route(type);
    }
}
