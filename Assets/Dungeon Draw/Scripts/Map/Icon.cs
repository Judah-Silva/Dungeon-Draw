using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public int type;
    private IconManager _iconManager;

    private Renderer _renderer;

    public void init(int type)
    {
        this.type = type;
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetFloat("_Glossiness", .5f);
        _iconManager = IconManager.Instance;
        SetColor();
    }

    public void SetColor()
    {
        switch (type)
        {
            case 0:
                _renderer.material.color = Color.red;
                break;
            case 1:
                _renderer.material.color = new Color(1, 0.8838954f, 1, 1);
                break;
            case 2:
                _renderer.material.color = new Color(0, 0.5726497f, 1, 1);
                break;
            case 3:
                _renderer.material.color = Color.green;
                break;
            case 4:
                _renderer.material.color = Color.red;
                break;
            case 5:
                _renderer.material.color = Color.gray;
                break;
        }
    }
    
    private void OnMouseDown()
    {
        _iconManager.Route(type);
    }
}
