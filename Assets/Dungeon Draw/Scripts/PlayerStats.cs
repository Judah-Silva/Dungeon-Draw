using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TotalDeckSize = 12;
    public static int HandSize = 5;
    public static List<int> PlayerDeck;
    
    void Start()
    {
        PlayerDeck = new List<int>();
        for (int i = 0; i < TotalDeckSize; i++)
        {
            PlayerDeck.Add(i);
        }
        // Debug.Log(PlayerDeck == null);
    }
}
