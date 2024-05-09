using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    public static CardManager Instance { get; private set; }
    
    public int maxMana = 3;
    public int currentMana;

    public GameObject player;

    private ActualCard selectedCard;
    private GameObject selectedGameObject;
    private Entity selectedEntity;
    private HandController _handController;

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

    private void Start()
    {
        _handController = GetComponent<HandController>();
    }

    public void ResetMana()
    {
        currentMana = maxMana;
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
        if (selectedCard == null || selectedCard != card)
        {
            selectedCard = card;
            // Debug.Log($"{card.cardID} has been selected");
        }
        else if (selectedCard == card)
        {
            selectedCard = null;
            // Debug.Log($"{card.cardID} has been deselected");
        }
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
                // Debug.Log("Enemy targeted");
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
        if (selectedCard == null)
        {
            return false;
        }

        // check if the selected game object is a valid target

        // Then check if the cards mana cost <= the players mana cost
        if (selectedCard.manaCost > currentMana)
        {
            return false;
        }

        currentMana -= selectedCard.manaCost;
        Player.Instance.UpdateManaBar();

        // Finally calls isplayable from card
        if (!selectedCard.isPlayable())
        {
            return false;
        }

        // If all of these are good, run the cards dealBlocks
        
        Debug.Log($"{currentMana} mana remaining.");
        
        selectedCard.dealBlocks(player, selectedGameObject);
        
        // Debug.Log($"Enemy: {selectedGameObject.name} targeted. Remaining health: {selectedEntity.getHP()}");
        
        _handController.RemoveCard(selectedCard);
        Discard.DiscardCard(selectedCard);

        return true;
    }
}
