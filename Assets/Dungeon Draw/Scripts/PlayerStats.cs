using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TotalDeckSize;
    public static List<int> PlayerDeck;
    public List<int> deck;
    
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            deck.Add(i);
        }

        TotalDeckSize = deck.Count;
        // Debug.Log(TotalDeckSize);

        PlayerDeck = deck;
    }
}
