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
using Quaternion = UnityEngine.Quaternion;

[System.Serializable]
public class shopItem
{
    public void setCard(List<int> card, GameObject cardPref)
    {
        if (ac == null) //used for changing the card if it is bought
        {
            GameObject g = GameObject.Instantiate(cardPref, Slot.transform);
            g.transform.localScale *= 75;
            g.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            ac = g.GetComponent<ActualCard>();
        }

        ac.CreateNewCard(card);
        ac.isShopItem = true;
        this.price = ac.cardVal;
        fillSlot();
    }

    public void setRelic(){}
    
    public void setBooster(){}


    public void fillSlot()
    {
        Slot.GetComponentInChildren<TMP_Text>().text = price.ToString();
        
    }
    public int price;
    public int ID;
    public Texture image;
    public GameObject Slot;
    public ActualCard ac;
    //public relic r;
    //public booster b;
}

public class Shop : MonoBehaviour
{
    public GameObject cardPrefab;


    public int cardSlotAmount = 5;
    public List<GameObject> cardSlots; //Takes gameobjects from scene for card slots and booster slots
    public List<GameObject> relicSlots; //Same but for relic slots
    public shopItem[] cardShopItems = new shopItem[5]; //Hold the actual cards
    public shopItem[] boosterPacks = new shopItem[2];
    public shopItem[] relics = new shopItem[6];

    public GameObject flashPanel;
    public Image img;
    public AnimationCurve curve;

    public GameObject gameManager;
    public SceneRouter sceneRouter;

    public string shopFileName = "shop";
    private List<string> shopRows = new List<string>();
    private int cMax, rMax,  bMax; //Values to know where in the stack cards/relics/boosterpacks end || Set in the loadShopItems function
    void Start()
    {
        //Find GameManager
        if (GameObject.Find("Game Manager"))
        {
            gameManager = GameObject.Find("Game Manager");
            sceneRouter = gameManager.GetComponent<SceneRouter>();
        }

        // Debug.Log("Shop start called");
        loadShopItemsFromText(); // Loads shopRows stack from shop.txt inside the resource folder and then populate shop
        
        //This line was really only needed in my testing.
        CardDataBase.allCards.Clear();
        if (!(CardDataBase.allCards.Count > 0)){CardDataBase.prePopulate();}
        
         int ran = 0;
         for (int i = 0; i<cardShopItems.Length; i++)
         {
            cardShopItems[i].Slot = cardSlots[i];
            ran = Random.Range(0, CardDataBase.allCards.Count);
            Debug.Log(CardDataBase.allCards.Count);
            cardShopItems[i].setCard(CardDataBase.getCard(ran), cardPrefab);
            //getPriceFromText(i, ran);
         }

         for (int i = 5; i < 13; i++)
         {
            getPriceFromText(i, -1); 
         }

         sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    public void CardPressed(int buttin)
    {
        //Debug.Log("Card "+ buttin +" Pressed");
        if (PlayerStats.Coins >= cardShopItems[buttin].price) // if enough money
        {
             PlayerStats.Coins -= cardShopItems[buttin].price;
             PlayerStats.Deck.Add(cardShopItems[buttin].ac.cardID); 
             PlayerStats.TotalDeckSize++;//Add to totalDeckSize??
             Debug.Log("Card bought - " + cardShopItems[buttin].ac.cardID);
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
        Debug.Log(boosterPacks[buttin].price);
        if (PlayerStats.Coins >= boosterPacks[buttin].price) // if enough money
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
        Debug.Log(relics[buttin].price);
        if (PlayerStats.Coins >= relics[buttin].price) // if enough money
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
        sceneRouter.ToMap();
    }

    private void repopulateShopItem(int shopId)
    {
        int ran = 0;
        ran = Random.Range(0, CardDataBase.allCards.Count);
        cardShopItems[shopId].setCard(CardDataBase.getCard(ran), null);
        //getPriceFromText(shopId, ran);
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

        
    }

    private void getPriceFromText(int i, int ran)
        {
            int priceMinIndex, priceMaxIndex, priceMin, priceMax;
                    
                if (i < 5)
                {
                    //ran = Random.Range(1, cMax); //picks random card
                    ran += 1;
                    Debug.Log(shopRows[ran]);
                    priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                    priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                    priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
                        shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                    priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
                        shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                    cardShopItems[i].price = Random.Range(priceMin, priceMax + 1);
                    Debug.Log(cardShopItems[i].price);
    
                    //After price is dealt with, we first assign a slot in the shop, then adjust visuals
                    //cardShopItems[i].Slot = cardSlots[i];
                    cardShopItems[i].fillSlot();
                }
                else if (i < 7)
                {
                    ran = Random.Range(cMax + 1, bMax); //picks random booster pack
                    Debug.Log(shopRows[ran]);
                    priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                    priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                    priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
                        shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                    priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
                        shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                    boosterPacks[i % 5].price = Random.Range(priceMin, priceMax + 1);
                    Debug.Log(boosterPacks[i % 5].price);
    
                    boosterPacks[i % 5].Slot = cardSlots[i];
                    boosterPacks[i % 5].fillSlot();
    
                }
                else if (i < 13)
                {
                    ran = Random.Range(bMax + 1, rMax + 1); // picks random relic
                    Debug.Log(shopRows[ran]);
                    priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
                    priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
                    priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
                        shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
                    priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
                        shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
                    relics[i % 7].price = Random.Range(priceMin, priceMax + 1);
                    Debug.Log(relics[i % 7].price);
    
                    relics[i % 7].Slot = relicSlots[i % 7];
                    relics[i % 7].fillSlot();
                }
        }
    

}
