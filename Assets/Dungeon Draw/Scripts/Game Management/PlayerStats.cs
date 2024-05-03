using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TotalDeckSize = 12;
    public static int HandSize = 5;
    public static int Coins = 100;
    public static int Glue = 0;
    public static int Tape = 3;
    public static int CurrentHealth = 50;
    public static int MaxHealth = 50;
    public static List<int> Deck = new List<int>();
    
    void Start()
    {
        //Deck = new List<int>(); // This was removing the ability to add cards to deck.. Unless there was another way and I'm dumb lol - Erik
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

    public void UpdateHealth(int modifier)
    {
        CurrentHealth = modifier;
    }
}
