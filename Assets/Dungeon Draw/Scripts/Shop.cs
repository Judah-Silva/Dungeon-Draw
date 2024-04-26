using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Numerics;
using TMPro;
using Random = UnityEngine.Random;

[System.Serializable]
public class shopItem
{
    public int price;
    public int ID;
    public Texture image;
    public GameObject Slot;
}
public class Shop : MonoBehaviour
{
    public GameObject card;

    public int cardSlotAmount = 5;
    public List<GameObject> cardSlots;
    public List<GameObject> relicSlots;
    public shopItem[] cardShopItems = new shopItem[5];
    public shopItem[] boosterPacks = new shopItem[2];
    public shopItem[] relics = new shopItem[6];

    public string MapSceneName = "Map";

    public string shopFileName = "shop";
    public List<string> shopRows = new List<string>();
    private int cMax, rMax,  bMax; //Values to know where in the stack cards/relics/boosterpacks end || Set in the loadShopItems function
    void Start()
    {
        cardSlotAmount = cardSlots.Count;
        // for (int i = 0; i < cardSlotAmount; i++) //This for loop places the card prefab onto the transform of the card slot as a child
        // {
        //     GameObject c = Instantiate(card, cardSlots[i]);
        //     c.transform.localScale  *= 100;
        // }
        //alternately, can just set the sprite of the cardSlot rather than placing a prefab. Not sure what will work better yet


        loadShopItems(); // Loads shopRows stack from shop.txt inside the resource folder
        // Now randomly selects cards/relics/boosterpacks
        // Debug.Log("Card Max - " + cMax);
        // Debug.Log("Boost Max - " + bMax);
        // Debug.Log("Relic Max - " + rMax);
        int ran, priceMinIndex, priceMaxIndex, priceMin, priceMax;
        for (int i = 0; i < 13; i++)
        {
            if (i < 5)
            {
                ran = Random.Range(1, cMax); //picks random card
                Debug.Log(shopRows[ran]);
                priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex, shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex, shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                cardShopItems[i].price = Random.Range(priceMin, priceMax + 1);
                Debug.Log(cardShopItems[i].price);
                
                //After price is dealt with, we first assign a slot in the shop, then adjust visuals
                cardShopItems[i].Slot = cardSlots[i];
                cardShopItems[i].Slot.GetComponentInChildren<TMP_Text>().text = cardShopItems[i].price.ToString();
            }
            else if (i < 7)
            {
                ran = Random.Range(cMax+1, bMax); //picks random booster pack
                Debug.Log(shopRows[ran]);
                priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex, shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex, shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                boosterPacks[i%5].price = Random.Range(priceMin, priceMax + 1);
                Debug.Log(boosterPacks[i%5].price);

                boosterPacks[i % 5].Slot = cardSlots[i];
                boosterPacks[i%5].Slot.GetComponentInChildren<TMP_Text>().text = boosterPacks[i%5].price.ToString();
                
            }
            else if (i <13)
            {
                ran = Random.Range(bMax + 1, rMax + 1); // picks random relic
                Debug.Log(shopRows[ran]);
                priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex, shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex, shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                relics[i%7].price = Random.Range(priceMin, priceMax + 1);
                Debug.Log(relics[i%7].price);
                
                relics[i % 7].Slot = relicSlots[i%7]; 
                relics[i%7].Slot.GetComponentInChildren<TMP_Text>().text = relics[i%7].price.ToString();
            }
        }
    }

    public void CardPressed()
    {
        Debug.Log("Card Pressed");
    }

    public void BoosterPressed()
    {
        Debug.Log("Booster Pressed");
    }

    public void RelicPressed()
    {
        Debug.Log("Relic Pressed");
    }

    public void RemoveCard()
    {
        Debug.Log("Remove Card Pressed");
    }

    public void leaveScene()
    {
        SceneManager.LoadScene(MapSceneName);
    }

    private void loadShopItems()
    {
        string shopFile = $"{Application.dataPath}{"/Dungeon Draw/Resources/"}{shopFileName}.txt";
        Debug.Log($"Loading shop file: {shopFile}");

  
        using (StreamReader sr = new StreamReader(shopFile))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                shopRows.Add(line);
                //Debug.Log("Line pushed - " + line);
                if (line == "[RELICS]")
                    bMax = shopRows.Count - 1;
                if (line == "[BOOSTER PACKS]")
                    cMax = shopRows.Count - 1;
            }

            rMax = shopRows.Count - 1;

            sr.Close();
        }
        // Debug.Log(shopRows);
    }

}
