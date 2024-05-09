using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public GameObject deckList;
    public GameObject buttonPrefab;
    

    public void Update()
    {
        healthText.text = "Health: " + PlayerStats.CurrentHealth + "/" + PlayerStats.MaxHealth;
        goldText.text = "Gold: " + PlayerStats.Coins;
        
        // clear existing buttons
        ClearDeckList();

        foreach (int card in PlayerStats.Deck)
        {
            CreateCardButton(card);
        }
    }

    void ClearDeckList()
    {
        foreach (Transform child in deckList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void CreateCardButton(int cardID)
    {
        GameObject newButton = Instantiate(buttonPrefab, deckList.transform);
        newButton.GetComponent<TextMeshProUGUI>().text = cardID.ToString();
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
    }
}
