using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class ActualCard : MonoBehaviour
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

    private CardManager cardManager;

    public Block[] blockArray;

    private GameObject Player;

    private TMP_Text block1;

    private TMP_Text block2;

    private TMP_Text manaCostText;

    private TMP_Text cardNameText;

    public Color c;
    private Renderer rend;
    private Animator anim;
    public float moveAmount = 1;
    private bool selected = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        RandomColor();
        cardManager = CardManager.Instance;
    }

    public void CreateNewCard(List<int> cardInfo)
    {
        init(cardInfo);
    }

    public void CreateNewCard(int cardId)
    {
        init(CardDataBase.getCard(cardId));
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

    //This method will be the main way to enact card effects, as it goes down the line to effect
    public void dealBlocks(GameObject Origin, GameObject Target)
    {

        Entity target = Target.GetComponent<Entity>();
        Entity origin = Origin.GetComponent<Entity>();

        for (int i = 0; i < blockArray.Length; i++)
        {
            Block block = blockArray[i];
            block.dealBlock(origin, target);
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

    public void OnMouseDown()
    {
        cardManager.SetCard(this);
    }

    void OnMouseEnter()
    {
        c.r -= .1f;
        c.g -= .1f;
        c.b -= .1f;
        rend.material.color = c;

        //My weird attempt at a card hover animation
        if (!selected)
            for (float i = moveAmount; i > 0; i -= .01f)
            {
                //transform.Translate(transform.position.x, transform.position.y + i, transform.position.z);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + i,
                    transform.localPosition.z - i);
            }
    }

    void OnMouseExit()
    {
        c.r += .1f;
        c.g += .1f;
        c.b += .1f;
        rend.material.color = c;

        //moves the card back into the hand
        if (!selected)
        {
            for (float i = moveAmount; i > 0; i -= .01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - i,
                    transform.localPosition.z + i);
            }
        }
    }

    void RandomColor()
    {
        c.r = Random.Range(0f, 1f);
        c.g = Random.Range(0f, 1f);
        c.b = Random.Range(0f, 1f);
        //Debug.Log(c.r + " " + c.g +  " "+ c.b);
        rend.material.color = c;
    }

}
