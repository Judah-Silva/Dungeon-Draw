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
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        DeckPile = PlayerStats.PlayerDeck;
        deckSize = DeckPile.Count;

        Shuffle(DeckPile);

        for (int i = 0; i < deckSize; i++)
        {
            Debug.Log(DeckPile[i]);
        }
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject spawn = HandController.positionsList[i];
            DrawCard(spawn);
        }
    }

    void DrawCard(GameObject location)
    {
        GameObject newCard = Instantiate(cardPrefab, location.transform.position, Quaternion.identity);
        Card cardComponent = newCard.GetComponent<Card>();
        cardComponent.SetCardProperties(DeckPile[0]);

        DeckPile.RemoveAt(0);
        // Debug.Log("pile count = " + DeckPile.Count);
        // Debug.Log(DeckPile[0]);
        // int index = i++;
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
