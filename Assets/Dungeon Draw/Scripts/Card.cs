using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Targeting targetingManager;

    void Start()
    {
        targetingManager = Targeting.Instance;
    }
    
    public void SetCard()
    {
        targetingManager.SetCard(this);
        // Debug.Log("event triggered");
    }

    // Need PlayCard to not be static
    // PlayCard should take the targets in as a parameter
    public void PlayCard(GameObject card)
    {
        Discard.currentSize++;
        Hand.currentSize--;
        
        // do card effect
        
        Debug.Log("Card played");
        
        // Should destroy this.gameObject, so that card does not need to be passed in
        Destroy(card);

        if (Hand.currentSize == 0)
        {
            Draw.DrawFromPile();
        }
    }
}
