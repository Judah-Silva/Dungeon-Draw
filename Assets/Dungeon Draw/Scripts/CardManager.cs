using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public int maxMana = 3;
    public int currentMana;

    public ActualCard selectedCard;
    public GameObject selectedGameObject;
    public Entity selectedEntity;


    public int getMana()
    {
        return currentMana;
    }

    public List<int> getCard(int cardId)
    {
        return CardDataBase.getCard(cardId);
    }

    public bool isValid(GameObject selectedGameObject)
    {

        // First check if there is a selected card

        // check if the selected game object is a valid target

        // Then check if the cards mana cost <= the players mana cost

        // Finally calls isplayable from card

        // If all of these are good, run the cards dealBlocks

        return true;

    }
}
