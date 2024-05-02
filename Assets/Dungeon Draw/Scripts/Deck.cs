using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static List<int> DeckPile;
    public int deckSize = 0;

    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        DeckPile = new List<int>();
        DeckPile.Clear();
        foreach (int cardId in PlayerStats.Deck)
        {
            DeckPile.Add(cardId);
        }
        deckSize = DeckPile.Count;
        Shuffle(DeckPile);
    }

    public void RefreshDeck(List<int> cards)
    {
        DeckPile.Clear();
        foreach (int cardID in cards)
        {
            AddCard(cardID);
        }

        deckSize = DeckPile.Count;
        Shuffle(DeckPile);
    }

    public GameObject DrawCard()
    {
        GameObject newCard = Instantiate(cardPrefab);
        newCard.SetActive(false);

        ActualCard cardComponent = newCard.GetComponent<ActualCard>();
        cardComponent.CreateNewCard(DeckPile[0]);

        DeckPile.RemoveAt(0);
        deckSize--;
        // Debug.Log("pile count = " + DeckPile.Count);
        // Debug.Log(DeckPile[0]);
        // int index = i++;

        return newCard;
    }

    public void AddCard(int cardId)
    {
        DeckPile.Add(cardId);
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}