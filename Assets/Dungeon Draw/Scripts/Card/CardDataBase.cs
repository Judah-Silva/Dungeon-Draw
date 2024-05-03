using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    //This class will do two things
    //First, it will store all of our cards as an array of ints
    //Second, it will have a method to get access to those arrays by the cardID

    public static List<List<int>> allCards = new List<List<int>>(); //All of the cards that could exist
                                                                    //Unsure if static is okay here, it should be because there should never be a CardDataBase object, but im bad with static so idk

    public static List<List<int>> heldCards = new List<List<int>>();

    public static void prePopulate()
    {
        //add all cards to allCards;
        allCards.Add(new List<int> { 0, 0, 0, 0, 1, 1, 1, 1, 0 });
        allCards.Add(new List<int> { 1, 1, 0, 0, 2, 1, 1, 0, 6 });
        allCards.Add(new List<int> { 2, 1, 0, 0, 2, 1, 1, 1, 6 });
<<<<<<< Updated upstream

=======

        allCards.Add(new List<int> { 3, 2, 0, 0, 2, 1, 1, 0, 6, 1, 1, 0, 4 });

        // Add all cards to heldCards for testing purposes
        heldCards.Add(allCards[0]);
        heldCards.Add(allCards[1]);
        heldCards.Add(allCards[2]);
        heldCards.Add(allCards[3]);
    }
    private void Start()
    {
        prePopulate();
>>>>>>> Stashed changes
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
        temp.Add(card[6]);
        for (int i = 7; i < card.Count; i++)
        {
            temp.Add(card[i]);
            temp.Add(card[i++]);
        }
        return temp;
    }
    public static List<string> getCardInfo(int cardId)
    {
        // Read some text file with all the text info of a card
        return null;
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
