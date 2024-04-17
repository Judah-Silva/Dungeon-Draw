using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static void PlayCard(GameObject card)
    {
        Discard.currentSize++;
        Hand.currentSize--;
        
        // do card effect
        
        Destroy(card);

        if (Hand.currentSize == 0)
        {
            Draw.DrawFromPile();
        }
    }
}
