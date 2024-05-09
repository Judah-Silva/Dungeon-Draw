using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TotalDeckSize = 12;
    public static int HandSize = 5;
    public static int Coins = 1000;
    public static int Glue = 0;
    public static float Tape = 2;
    public static float MaxTape = 3f;
    public static int CurrentHealth = 50;
    public static int MaxHealth = 50;
    public static List<int> Deck = new List<int>();
    public static List<Relic> Relics = new List<Relic>();

    public delegate void TapeChange();
    public static event TapeChange OnTapeChange;
    
    public delegate void GlueChange();
    public static event GlueChange OnGlueChange;
    
    void Start()
    {
        //Deck = new List<int>(); // This was removing the ability to add cards to deck.. Unless there was another way and I'm dumb lol - Erik
        // PopulateDeck();
    }

    // Populate deck with 12 cards from database for testing purposes
    public void PopulateDeck()
    {
        List<int> defaultDeck = new List<int>();
        
        // Don't add these because it breaks things
        // defaultDeck.Add(0);
        // defaultDeck.Add(0);
        
        defaultDeck.Add(1);
        defaultDeck.Add(1);
        defaultDeck.Add(1);
        defaultDeck.Add(1);
        
        defaultDeck.Add(2);
        defaultDeck.Add(2);
        defaultDeck.Add(2);
        defaultDeck.Add(2);
        
        defaultDeck.Add(3);
        defaultDeck.Add(3);

        Deck = defaultDeck;
    }

    public void UpdateHealth(int modifier)
    {
        if (modifier > MaxHealth)
            modifier = MaxHealth;
        else if (modifier < 0)
        {
                modifier = 0;
        }

        CurrentHealth = modifier;
    }

    public bool checkForRelic(int id)
    {
        if (Relics.Count!=0)
            foreach (Relic r in Relics)
            {
                if (r.id == id)
                    return true;
            }

        return false;
    }

    public void addRelic(Relic r)
    {
        Relics.Add(r);
        r.PerformFunction();
        if (GameObject.Find("Top Info"))
        {
            GameObject.Find("Top Info").GetComponent<InfoManager>().updateRelics(r);
        }
    }
    
    public void removeRelic(Relic r)
    {
        if (GameObject.Find("Top Info"))
        {
            GameObject.Find("Top Info").GetComponent<InfoManager>().removeRelicUI(Relics.IndexOf(r));
        }
        Relics.Remove(r);
    }

    public void GainTape(float tapeAmount)
    {
        Tape = Math.Min(Tape + tapeAmount, MaxTape);
        OnTapeChange?.Invoke();
    }
    
    public void GainGlue(int glueAmount)
    {
        Glue += glueAmount;
        OnGlueChange?.Invoke();
    }
}
