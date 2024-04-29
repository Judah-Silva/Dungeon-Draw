using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    // Define one instance for other scripts to use
    public static Targeting Instance { get; private set; }
    
    private Card _card;
    
    // Will be an array for multiple enemy selection
    private Enemy _enemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetCard(Card card)
    {
        if (_card == card)
        {
            _card = null;
            Debug.Log("Card deselected");
        }
        else
        {
            _card = card;
            Debug.Log("Card selected");
        }
    }

    public void SetTarget(Enemy enemy)
    {
        if (_card != null)
        {
            _enemy = enemy;
            // Add checks for if card has multiple targets
            _card.PlayCard();
            Debug.Log($"Card played on {enemy.name}");
        }
        else
        {
            Debug.Log("No card selected!");
        }
    }
}
