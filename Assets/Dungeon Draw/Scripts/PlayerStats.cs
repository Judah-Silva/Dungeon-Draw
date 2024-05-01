using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TotalDeckSize = 12;
    public static int HandSize = 5;
    public static List<int> Deck;
    
    void Start()
    {
        Deck = new List<int>();
        PopulateDeck();
    }

    // Populate deck with 12 cards from database for testing purposes
    void PopulateDeck()
    {
        Deck.Add(0);
        Deck.Add(0);
        
        Deck.Add(1);
        Deck.Add(1);
        Deck.Add(1);
        Deck.Add(1);
        
        Deck.Add(2);
        Deck.Add(2);
        Deck.Add(2);
        Deck.Add(2);
        
        Deck.Add(3);
        Deck.Add(3);
    }
}
