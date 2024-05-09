using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    //This class will do two things
    //First, it will store all of our cards as an array of ints
    //Second, it will have a method to get access to those arrays by the cardID

    // Card structure: [ cardId, manaCost, cardVal, rarity, numBlocks, numBlocks Times -> <condition, numEffects, numEffects times -> <effectType, effectVal> > ]
    
    public static List<List<int>> allCards = new List<List<int>>(); //All of the cards that could exist
                                                                    //Unsure if static is okay here, it should be because there should never be a CardDataBase object, but im bad with static so idk

    public static List<List<int>> heldCards = new List<List<int>>();

    public static void prePopulate()
    {
        //add all cards to allCards; //Current allCards.Count: 31
        //Null card
        allCards.Add(new List<int> { 0, 0, 0, 0, 1, 1, 1, 1, 0 });
        //Basic cards
        allCards.Add(new List<int> { 1, 1, 5, 0, 1, 1, 1, 0, 6 });
        allCards.Add(new List<int> { 2, 1, 5, 0, 1, 1, 1, 1, 6 });

        //Common Cards (Some are duplicate intenionally, as there are just more unique attack cards then defend cards, so some of the defend cards are duplicated
        allCards.Add(new List<int> { 3, 1, 15, 0, 1, 1, 1, 0, 8 });
        allCards.Add(new List<int> { 4, 1, 15, 0, 1, 1, 1, 1, 8 });
        allCards.Add(new List<int> { 5, 1, 15, 0, 1, 1, 1, 2, 2 });
        allCards.Add(new List<int> { 6, 1, 15, 0, 1, 1, 1, 3, 2 });
        allCards.Add(new List<int> { 7, 1, 15, 0, 1, 1, 1, 4, 2 });
        allCards.Add(new List<int> { 8, 1, 15, 0, 1, 1, 1, 6, 6 });
        allCards.Add(new List<int> { 9, 1, 15, 0, 1, 1, 1, 7, 2 });
        allCards.Add(new List<int> { 10, 1, 15, 0, 1, 1, 1, 5, 1 });
        allCards.Add(new List<int> { 11, 1, 15, 0, 1, 1, 1, 1, 8 });
        allCards.Add(new List<int> { 12, 1, 15, 0, 1, 1, 1, 1, 8 });
        allCards.Add(new List<int> { 16, 2, 15, 0, 1, 1, 1, 1, 18 });
        allCards.Add(new List<int> { 17, 2, 15, 0, 1, 1, 1, 0, 18 });
        allCards.Add(new List<int> { 25, 1, 15, 0, 1, 1, 1, 6, 7 });

        //Uncommon Cards
        allCards.Add(new List<int> { 13, 1, 30, 1, 2, 1, 1, 0, 8, 1, 1, 2, 2 });
        allCards.Add(new List<int> { 14, 1, 30, 1, 2, 1, 1, 0, 8, 1, 1, 3, 2 });
        allCards.Add(new List<int> { 15, 1, 30, 1, 2, 1, 1, 0, 8, 1, 1, 4, 2 });
        allCards.Add(new List<int> { 19, 2, 30, 1, 1, 1, 1, 7, 4 });
        allCards.Add(new List<int> { 20, 1, 30, 1, 2, 1, 1, 1, 20, 1, 1, 3, 1 });
        allCards.Add(new List<int> { 21, 1, 30, 1, 2, 1, 1, 0, 20, 1, 1, 4, 1 });
        allCards.Add(new List<int> { 23, 1, 30, 1, 2, 1, 1, 2, 3, 1, 1, 3, 3});
        allCards.Add(new List<int> { 24, 2, 30, 1, 2, 1, 1, 0, 10, 1, 1, 3, 5 });
        allCards.Add(new List<int> { 26, 1, 30, 1, 1, 1, 1, 6, 10});
        allCards.Add(new List<int> { 29, 1, 30, 1, 2, 1, 1, 1, 30, 1, 1, 4, 2 });

        //Rare Cards
        allCards.Add(new List<int> { 18, 3, 50, 2, 1, 1, 1, 7, 7 });
        allCards.Add(new List<int> { 22, 1, 50, 2, 1, 1, 1, 0, 20 });
        allCards.Add(new List<int> { 27, 1, 50, 2, 1, 1, 1, 1, 20 });
        allCards.Add(new List<int> { 28, 1, 50, 2, 1, 1, 1, 6, 13});
        allCards.Add(new List<int> { 30, 1, 50, 2, 2, 1, 1, 7, 1, 1, 2, 3, 2, 2, 2});
        // Test card
        //allCards.Add(new List<int> { 3, 2, 30, 0, 2, 1, 1, 0, 6, 1, 1, 0, 4 });

        // Add all cards to heldCards for testing purposes
        //heldCards.Add(allCards[0]);
        //heldCards.Add(allCards[1]);
        //heldCards.Add(allCards[2]);
        //heldCards.Add(allCards[3]);
    }

    private void Start()
    {
        prePopulate();
    }

    public static List<int> getCard(int cardId)
    {
        List<int> card;
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i][0] == cardId)
            {
                card = allCards[i];
                return card;
            }
        }
        Debug.Log("Card does not exist");
        return null;
    }

    // Returns the block of a card at a given id
    public static List<int> getBlockAtId(int cardId)
    {
        List<int> card = getCard(cardId);

        List<int> temp = new List<int>();

        for (int i = 7; i < card.Count; i += 2)
        {
            temp.Add(card[i]);
            temp.Add(card[i + 1]);
        }

        return temp;
    }

    public static List<string> getCardInfo(int cardId)
    {
        // Read some text file with all the text info of a card
        return null;
    }

    public static void checkDataBase()
    {
        foreach (var heldCards in allCards)
        {
            // Sanity check
            Debug.Log("Start of card:");

            foreach (var card in heldCards)
            {
                Debug.Log(card);
            }

            Debug.Log("End of card");

        }
    }

    public static int GetDeckSize()
    {
        return heldCards.Count;
    }

    public static string getCardName(int id)
    {

        string temp = "";

        switch (id)
        {
            case 0:
                temp = "Test card";
                break;
            case 1:
                temp = "Damage 1";
                break;
            case 2:
                temp = "Shield 1";
                break;
            case 3:
                temp = "Damage 2";
                break;
            default:
                temp = "NYI";
                break;
        }

        return temp;

    }
}
