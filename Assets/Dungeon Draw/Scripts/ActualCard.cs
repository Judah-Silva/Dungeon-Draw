using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ActualCard : ScriptableObject
{
    public string cardName;

    public int cardID;

    public String image; //Varible type not confirmed

    public int manaCost;

    public int cardVal;

    public int rarity;

    public int numOfEffects;
    public int condition;
    public int effectType;
    public int effectVal;

    public int offset = 5;

    public CardManager cardManager;

    public Block[] blockArray;

    public GameObject Player;

    public TMP_Text block1;

    public TMP_Text block2;

    public TMP_Text manaCostText;

    public TMP_Text cardNameText;



    public ActualCard(List<int> cardInfo)
    {

        init(cardInfo);

    }

    public ActualCard()
    {
        init(CardDataBase.getCard(0));
    }

    private void init(List<int> cardInfo)
    {

        cardID = cardInfo[0];
        manaCost = cardInfo[1];
        cardVal = cardInfo[2];
        rarity = cardInfo[3];

        int numOfBlocks = cardInfo[4];


        blockArray = new Block[2]; //Length should always be 2

        // For the number of blocks in a card
        for (int i = 0; i < numOfBlocks; i++)
        {

            // First gather the condition of the card
            condition = cardInfo[offset];
            offset++;

            // if it's 0, it'll break out of the
            if (condition != 0)
            {

                numOfEffects = cardInfo[offset];
                offset++;


                for (int j = 0; j < numOfEffects; i++)
                {

                    effectType = cardInfo[offset];
                    offset++;
                    effectVal = cardInfo[offset];
                    offset++;

                    blockArray[j].addEffect(effectType, effectVal);

                }

            }

        }

    }


    //A method to check if the card is playable due to manacost of the player and the card itself,
    // while also passing it down to block to check those isPlayables as well.
    public bool isPlayable()
    {
        if (manaCost > cardManager.getMana())
        {
            return false;
        }
        else
        {
            for (int i = 0; i < blockArray.Length - 1; i++)
            {
                Block block = blockArray[i];
                return block.isPlayable();
            }
            return false;
        }
    }

    //This method will be the main way to enact card effects, as it goes down the line to efffect
    public void dealBlocks(GameObject Target)
    {

        Entity target = Target.GetComponent<Entity>();

        for (int i = 0; i < blockArray.Length; i++)
        {
            Block block = blockArray[i];
            block.dealBlock(target);
        }
    }

    //This method will remove and extra NON-Glued cards
    public void cleanUp()
    {
        if (blockArray[1].condition == 2)
        {
            blockArray[1].condition = 0;
        }
    }

    //Used for merging cards with glue or tape
    public void addBlock(int condition, int cardId)
    {
        int[] cardData = blockArray[1].copyBlock(condition, cardID);
        int numOfEffects = cardData[6];

        for (int i = 0; i < numOfEffects; i++)
        {
            //blockArray[1] = 
        }
    }


}
