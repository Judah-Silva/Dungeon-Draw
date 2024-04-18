using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Targeting targetingManager;
    
    void Start()
    {
        targetingManager = Targeting.instance;
    }

    private void OnMouseDown()
    {
        targetingManager.SetTarget(this);
    }
}
