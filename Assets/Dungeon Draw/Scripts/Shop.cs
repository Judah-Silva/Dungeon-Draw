using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Numerics;
using TMPro;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[System.Serializable]
public class shopItem
{
    public void setCard(List<int> card)
    {
        //ac = new ActualCard(card);
        fillSlot();
    }

    public void fillSlot()
    {
        Slot.GetComponentInChildren<TMP_Text>().text = price.ToString();
        
    }
    public int price;
    public int ID;
    public Texture image;
    public GameObject Slot;
    //public ActualCard ac = null;
}
public class Shop : MonoBehaviour
{
    public GameObject card;

    //public GameObject gameManager;
    //private CardDataBase = cardDB;
    //private RelicDataBase = relicDB;

    public int cardSlotAmount = 5;
    public List<GameObject> cardSlots; //Takes gameobjects from scene for card slots and booster slots
    public List<GameObject> relicSlots; //Same but for relic slots
    public shopItem[] cardShopItems = new shopItem[5]; //Hold the actual cards
    public shopItem[] boosterPacks = new shopItem[2];
    public shopItem[] relics = new shopItem[6];

    public GameObject flashPanel;
    public Image img;
    public AnimationCurve curve;

    public string MapSceneName = "Map";

    public string shopFileName = "shop";
    private List<string> shopRows = new List<string>();
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


        loadShopItemsFromText(); // Loads shopRows stack from shop.txt inside the resource folder and then populate shop
        
        //Preparation for CardDataBase
        /*gameManager = GameObject.Find("GameManager");
         cardDB = gameManager.GetComponent<CardDataBase>();
         int ran = 0;
         for (int i = 0; i<cardShopItems.Length; i++)
         {
            cardShopItems[i].Slot = cardSlots[i];
            ran = Random.Range(0, cardDB.allCards.Count);
            cardShopItems[i].setCard(cardDB.getCard(ran));
         }
         
         */
    }

    public void CardPressed(int buttin)
    {
        Debug.Log("Card "+ buttin +" Pressed");
        //Check for money
        bool money = false;
        Debug.Log(cardShopItems[buttin].price);
        if (money) // if enough money
        {
             //money -= cardShopItems[buttin].price;
             //cardDB.heldCards.add(cardDB.getCard(cardShopItems[buttin].ac.cardID))
             repopulateShopItem(buttin);
        }
        else
        {
            StartCoroutine(declineFade());
        }
    }

    public void BoosterPressed(int buttin)
    {
        Debug.Log("Booster "+ buttin +" Pressed");
        bool money = false;
        Debug.Log(boosterPacks[buttin].price);
        if (money) // if enough money
        {
            
        }
        else
        {
            StartCoroutine(declineFade());
        }
    }

    public void RelicPressed(int buttin)
    {
        Debug.Log("Relic "+ buttin +" Pressed");
        bool money = false;
        Debug.Log(relics[buttin].price);
        if (money) // if enough money
        {
            
        }
        else
        {
            StartCoroutine(declineFade());
        }
    }

    public void RemoveCard()
    {
        Debug.Log("Remove Card Pressed");
    }

    public void leaveScene()
    {
        SceneManager.LoadScene(MapSceneName);
    }

    private void repopulateShopItem(int shopId)
    {
        // int ran = 0;
        // ran = Random.Range(0, cardDB.allCards.Count);
        // cardShopItems[shopId].setCard(cardDB.getCard(ran));
         
    }
    

    IEnumerator declineFade()
    {
        float t = .85f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(1f, 0f, 0f, a);
            yield return 0;
        }
    }

    private void loadShopItemsFromText()
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

}
