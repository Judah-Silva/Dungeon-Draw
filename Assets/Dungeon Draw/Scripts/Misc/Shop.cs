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
        
        GameObject g = GameObject.Instantiate(cardPref, Slot.transform);
        g.transform.localScale *= 75;
        g.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        ac = g.GetComponent<ActualCard>();

        ac.CreateNewCard(card);
        ac.isShopItem = true;
        this.price = ac.cardVal;
        fillSlot();
    }

    public void setRelic(Relic r)
    {
        rel = r;
        this.price = r.value;
        Debug.Log(r.art);
        this.image = r.art;
        fillSlot();
    }

    public void setBooster(){}


    public void fillSlot()
    {
        Slot.GetComponentInChildren<TMP_Text>().text = price.ToString();
        if (image != null)
        {
            Slot.GetComponent<RawImage>().texture = image.texture;
        }
        
    }
    public int price;
    public int ID;
    public Sprite image;
    public GameObject Slot;
    public ActualCard ac;

    public Relic rel;
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

    public Texture X;

    public GameObject flashPanel;
    public Image img;
    public AnimationCurve curve;

    public AudioSource src;
    public AudioClip purchaseClip;
    public AudioClip declineClip;

    public SceneRouter sceneRouter;
    public PlayerStats playerStats;

    public string shopFileName = "shop";
    private List<string> shopRows = new List<string>();
    private int cMax, rMax,  bMax; //Values to know where in the stack cards/relics/boosterpacks end || Set in the loadShopItems function
    void Start()
    {
        if (GameManager.Instance != null)
        {
            sceneRouter = GameManager.Instance.GetSceneRouter();
            playerStats = GameManager.Instance.GetPlayerStats();
        }
        
        //Check for slimy glasses relic
        if (playerStats != null && playerStats.checkForRelic(11))
            playerStats.UpdateHealth(PlayerStats.CurrentHealth+6);

        // Debug.Log("Shop start called");
        //loadShopItemsFromText(); // Loads shopRows stack from shop.txt inside the resource folder and then populate shop

        // Sprite s = Resources.Load<Sprite>("RelicArt/BigToe");
        // Debug.Log(s);
        
        //This line was really only needed in my testing.
        // CardDataBase.allCards.Clear();
        if ((CardDataBase.allCards.Count == 0)){CardDataBase.prePopulate();}
        if ((RelicDatabase.allRelics.Count == 0)){RelicDatabase.prePopulate();}
        //RelicDatabase.prePopulate();
        
         int ran = 0;
         for (int i = 0; i<cardShopItems.Length; i++)
         {
            cardShopItems[i].Slot = cardSlots[i];
            ran = Random.Range(1, CardDataBase.allCards.Count);
            Debug.Log(CardDataBase.allCards.Count);
            cardShopItems[i].setCard(CardDataBase.getCard(ran), cardPrefab);
            //getPriceFromText(i, ran);
         }
         int[] shopUsedRels = new int[6];

         for (int i = 0; i < 6; i++)
         {
             
            bool repeat = false;
             ran = 0;
                 relics[i].Slot = relicSlots[i];
                 if (i < 3) // This will set the top 3 relics as non commons and make sure no duplicates
                 {
                     while (RelicDatabase.getRelic(ran).rarity == rarityValues.common || RelicDatabase.getRelic(ran).rarity == rarityValues.unique || repeat == true)
                     {
                         ran = Random.Range(0, RelicDatabase.allRelics.Count);
                         Debug.Log(ran);
                         for (int l = 0; l < i; l++)
                         {
                             if (shopUsedRels[l] == ran)
                             {
                                 repeat = true;
                                 break;
                             }
                             else
                             {
                                 repeat = false;
                             }
                         }
                     }
                 }
                 else //Commons currently can be duplicate.
                 {
                     ran = 2; //defaulting to a rare rarity relic for while loop
                     while (RelicDatabase.getRelic(ran).rarity != rarityValues.common)
                     {
                         ran = Random.Range(0, RelicDatabase.allRelics.Count);
                         Debug.Log(ran);
                     }
                 }

                 shopUsedRels[i] = ran;
                 relics[i].setRelic(RelicDatabase.getRelic(ran));
         }

         //sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    public void CardPressed(int buttin)
    {
        //Debug.Log("Card "+ buttin +" Pressed");
        if (PlayerStats.Coins >= cardShopItems[buttin].price) // if enough money
        {
            src.clip = purchaseClip;
            src.Play();
            
             PlayerStats.Coins -= cardShopItems[buttin].price;
             PlayerStats.Deck.Add(cardShopItems[buttin].ac.cardID); 
             PlayerStats.TotalDeckSize++;//Add to totalDeckSize??
             Debug.Log("Card bought - " + cardShopItems[buttin].ac.cardID);
             Destroy(cardShopItems[buttin].Slot.transform.GetChild(2).gameObject);
             if (playerStats.checkForRelic(8)) // If player has penny relic then replenish item
                repopulateCardShopItem(buttin);
             else
             {
                 cardShopItems[buttin].Slot.GetComponent<RawImage>().texture = X;
                 cardShopItems[buttin].price = 99999;
                 cardShopItems[buttin].Slot.GetComponentsInChildren<RawImage>()[1].enabled = false;
                 cardShopItems[buttin].Slot.GetComponentInChildren<TMP_Text>().enabled = false;
             }
        }
        else
        {
            src.clip = declineClip;
            src.Play();
            
            StartCoroutine(declineFade());
        }
    }

    public void BoosterPressed(int buttin)
    {
        Debug.Log("Booster "+ buttin +" Pressed");
        Debug.Log(boosterPacks[buttin].price);
        if (PlayerStats.Coins >= boosterPacks[buttin].price) // if enough money
        {
            src.clip = purchaseClip;
            src.Play();
        }
        else
        {
            src.clip = declineClip;
            src.Play(); 
            
            StartCoroutine(declineFade());
        }
    }

    public void RelicPressed(int buttin)
    {
        Debug.Log("Relic "+ buttin +" Pressed");
        Debug.Log(relics[buttin].price);
        if (PlayerStats.Coins >= relics[buttin].price) // if enough money
        {
            src.clip = purchaseClip;
            src.Play();
            PlayerStats.Coins -=  relics[buttin].price;
            playerStats.addRelic(relics[buttin].rel);
            relics[buttin].Slot.GetComponent<RawImage>().texture = X;
            relics[buttin].price = 99999;
            relics[buttin].Slot.GetComponentsInChildren<RawImage>()[1].enabled = false;
            relics[buttin].Slot.GetComponentInChildren<TMP_Text>().enabled = false;
            relics[buttin].Slot.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_Text>().text =
                            ("<b>" + relics[buttin].rel.name + "</b>\n\nPURCHASED!!!");
        }
        else
        {
            src.clip = declineClip;
            src.Play(); 
            
            StartCoroutine(declineFade());
        }
    }

    public void RelicHovered(int buttin)
    {
        // Debug.Log("Relic : " + buttin + " Hovered");
        if (relics[buttin].price != 99999)
        {
            relics[buttin].Slot.transform.GetChild(2).gameObject.SetActive(true);
            relics[buttin].Slot.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_Text>().text =
                ("<b>" + relics[buttin].rel.name + "</b>\n" + relics[buttin].rel.description);
        }
    }

    public void RelicUnHovered(int buttin)
    {
        // Debug.Log("Relic : " + buttin + " UnHovered");
            relics[buttin].Slot.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void RemoveCard()
    {
        Debug.Log("Remove Card Pressed");
    }

    public void leaveScene()
    {
        sceneRouter.ToMap();
    }

    private void repopulateCardShopItem(int shopId)
    {
        int ran = 0;
        ran = Random.Range(1, CardDataBase.allCards.Count);
        
        cardShopItems[shopId].setCard(CardDataBase.getCard(ran), cardPrefab);
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

    // private void loadShopItemsFromText()
    // {
    //     string shopFile = $"{Application.dataPath}{"/Dungeon Draw/Resources/"}{shopFileName}.txt";
    //     Debug.Log($"Loading shop file: {shopFile}");
    //     
    //
    //     using (StreamReader sr = new StreamReader(shopFile))
    //     {
    //         string line = "";
    //         while ((line = sr.ReadLine()) != null)
    //         {
    //             shopRows.Add(line);
    //             //Debug.Log("Line pushed - " + line);
    //             if (line == "[RELICS]")
    //                 bMax = shopRows.Count - 1;
    //             if (line == "[BOOSTER PACKS]")
    //                 cMax = shopRows.Count - 1;
    //         }
    //
    //         rMax = shopRows.Count - 1;
    //
    //         sr.Close();
    //     }
    //
    //     
    // }
    //
    // private void getPriceFromText(int i, int ran)
    //     {
    //         int priceMinIndex, priceMaxIndex, priceMin, priceMax;
    //                 
    //             if (i < 5)
    //             {
    //                 //ran = Random.Range(1, cMax); //picks random card
    //                 ran += 1;
    //                 Debug.Log(shopRows[ran]);
    //                 priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
    //                 priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
    //                 priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
    //                     shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
    //                 priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
    //                     shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
    //                 cardShopItems[i].price = Random.Range(priceMin, priceMax + 1);
    //                 Debug.Log(cardShopItems[i].price);
    //
    //                 //After price is dealt with, we first assign a slot in the shop, then adjust visuals
    //                 //cardShopItems[i].Slot = cardSlots[i];
    //                 cardShopItems[i].fillSlot();
    //             }
    //             else if (i < 7)
    //             {
    //                 ran = Random.Range(cMax + 1, bMax); //picks random booster pack
    //                 Debug.Log(shopRows[ran]);
    //                 priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
    //                 priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
    //                 priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
    //                     shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
    //                 priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
    //                     shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
    //                 boosterPacks[i % 5].price = Random.Range(priceMin, priceMax + 1);
    //                 Debug.Log(boosterPacks[i % 5].price);
    //
    //                 boosterPacks[i % 5].Slot = cardSlots[i];
    //                 boosterPacks[i % 5].fillSlot();
    //
    //             }
    //             else if (i < 13)
    //             {
    //                 ran = Random.Range(bMax + 1, rMax + 1); // picks random relic
    //                 Debug.Log(shopRows[ran]);
    //                 priceMinIndex = shopRows[ran].IndexOf("Min:") + "Min:".Length;
    //                 priceMaxIndex = shopRows[ran].IndexOf("Max:") + "Max:".Length;
    //                 priceMin = int.Parse(shopRows[ran].Substring(priceMinIndex,
    //                     shopRows[ran].IndexOf(',', priceMinIndex) - priceMinIndex));
    //                 priceMax = int.Parse(shopRows[ran].Substring(priceMaxIndex,
    //                     shopRows[ran].IndexOf(';', priceMaxIndex) - priceMaxIndex));
    //                 relics[i % 7].price = Random.Range(priceMin, priceMax + 1);
    //                 Debug.Log(relics[i % 7].price);
    //
    //                 relics[i % 7].Slot = relicSlots[i % 7];
    //                 relics[i % 7].fillSlot();
    //             }
    //     }
    

}
