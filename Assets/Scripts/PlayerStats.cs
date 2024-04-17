using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int DeckSize;
    public int deck = 10;
    
    void Start()
    {
        DeckSize = deck;
    }
}
