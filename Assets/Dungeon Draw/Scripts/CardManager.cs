using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    public static CardManager Instance { get; private set; }
    
    public int maxMana = 3;
    public int currentMana;

    private ActualCard selectedCard;
    private GameObject selectedGameObject;
    private Entity selectedEntity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int getMana()
    {
        return currentMana;
    }

    public List<int> getCard(int cardId)
    {
        return CardDataBase.getCard(cardId);
    }

    public void SetCard(ActualCard card)
    {
        selectedCard = card;
    }

    public void SetTarget(GameObject enemy)
    {
        if (selectedCard == null)
        {
            Debug.Log("No selected card!");
        } else
        {
            selectedGameObject = enemy;
            selectedEntity = enemy.GetComponent<Entity>();

            if (isValid())
            {
                Debug.Log("Enemy targeted");
            }
            else
            {
                Debug.Log("Could not target enemy");
            }
        }
    }

    public bool isValid()
    {

        // First check if there is a selected card

        // check if the selected game object is a valid target

        // Then check if the cards mana cost <= the players mana cost

        // Finally calls isplayable from card

        // If all of these are good, run the cards dealBlocks

        selectedCard.dealBlocks(selectedGameObject);

        return true;

    }
}
