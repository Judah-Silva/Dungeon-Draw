using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class ActualCard : MonoBehaviour
{

    [Header("General Card Vars")]
    public int cardID;
    public string cardName;
    public int manaCost;
    public int cardVal;
    public int rarity;

    [Header("Other pt. 1")]
    public int[] numOfEffects; // A value for each block
    public int[] condition; // A value for each block
    public int[][] effectType; // A value for each effect in each block
    public int[][] effectVal; // A value for each effect in each block

    public int offset = 5;

    private CardManager cardManager;

    public Block[] blockArray;

    private GameObject Player;

    [Header("Text Elements")]
    public TMP_Text block1;
    public TMP_Text block2;
    public TMP_Text manaCostText;
    public TMP_Text cardNameText;

    [Header("Card Images && the associated gameobjects")]
    // May not need these
    public Image cardImage;
    public Image block1Background;
    public Image block2Background;
    // Used to hold the 3 variants of a block background
    public Texture2D baseBG;
    public Texture2D tapeBG;
    public Texture2D glueBG;
    // Game Objects that reference the actual objects holding the images
    public GameObject cardImageBox;
    public GameObject block1Box;
    public GameObject block2Box;

    [Header("Other pt. 2")]
    public Color c;
    private Renderer rend;
    private Animator anim;
    public float moveAmount = 1;
    public float hoverSmoothness = 5f;
    public Vector3 originalPosition;
    [HideInInspector]
    public bool isShopItem = false;
    public bool isRipped = false;

    public ActualCard(List<int> cardInfo)
    {
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject == gameObject)
            {
                // Move the object up smoothly
                Vector3 targetPosition = originalPosition + Vector3.up * moveAmount;
                transform.position = Vector3.Lerp(transform.position, targetPosition, hoverSmoothness * Time.deltaTime);
            }
            else if (transform.position != originalPosition)
            {
                // Move the object back to its original position if not hovering
                transform.position = Vector3.Lerp(transform.position, originalPosition, hoverSmoothness * Time.deltaTime);
            }

            // Used to check if we need to rip a card
            // Then allows it to be merged into another card
            if (Input.GetMouseButtonDown(1))
            {
                if (PlayerStats.Tape < 1)
                {
                    Debug.Log("Not enough tape");
                }
                else if (isRipped)
                {
                    Debug.Log("This card is already ripped");
                } else if (condition[1] != 0)
                {
                    Debug.Log("This card is already full");
                }
                else
                {
                    Debug.Log("Choose a card to attach to");
                    ripCard();
                }
            }
        }

        
    }

    public void CreateNewCard(List<int> cardInfo)
    {
        init(cardInfo);
    }

    public void CreateNewCard(int cardId)
    {
        List<int> cardInfo = CardDataBase.getCard(cardId);
        if (cardInfo == null)
        {
            Debug.Log("Could not create card");
            return;
        }
        init(cardInfo);
    }



    private void init(List<int> cardInfo)
    {
        cardID = cardInfo[0];
        manaCost = cardInfo[1];
        cardVal = cardInfo[2];
        rarity = cardInfo[3];

        int numOfBlocks = cardInfo[4];

        cardNameText.text = cardID.ToString(); //Temporary for shop testing


        blockArray = new Block[2]; //Length should always be 2
        condition = new int[2];
        numOfEffects = new int[2];

        effectType = new int[2][];
        effectType[0] = new int[5]; // temporary limit on effects per block
        effectType[1] = new int[5];

        effectVal = new int[2][];
        effectVal[0] = new int[5]; // temporary limit on effects per block
        effectVal[1] = new int[5];

        // For the number of blocks in a card
        for (int i = 0; i < numOfBlocks; i++)
        {
            if (offset >= cardInfo.Count)
            {
                break;
            }

            // First gather the condition of the card
            condition[i] = cardInfo[offset];
            offset++;

            // if it's 0, it'll break out of the
            if (condition[i] != 0)
            {
                blockArray[i] = new Block();

                numOfEffects[i] = cardInfo[offset];
                offset++;


                for (int j = 0; j < numOfEffects[i]; j++)
                {
                    if (offset >= cardInfo.Count)
                    {
                        break;
                    }
                    // effectType = [ block1 -> [1, 1], block2 -> [1] ]
                    effectType[i][j] = cardInfo[offset];
                    offset++;

                    // effectVal = [ block1 -> [6, 6], block2 -> [3] ]
                    effectVal[i][j] = cardInfo[offset];
                    offset++;

                    blockArray[i].addEffect(effectType[i][j], effectVal[i][j]);

                }

            }

        }

        updateVisuals();

    }
    // Function that updates the text and the images on a card
    private void updateVisuals()
    {

        // First the strings get updated

        // Makes a call to the card database as that's where all the card names get stored
        // Then the code will update the card name text
        cardName = CardDataBase.getCardName(cardID);
        cardNameText.text = cardName;

        // Then updates the mana value
        manaCostText.text = manaCost.ToString();

        // For both of the blocks, it wil first check if it exists, and if it does, then --- 
        // --- it will call the afformentioned block w/ the show block function
        if (condition[0] != 0)
        {
            block1.text = blockArray[0].showBlock();
        }
        else
        {
            block1.text = "";
        }

        if (condition[1] != 0)
        {
            block2.text = blockArray[0].showBlock();
        }
        else
        {
            block2.text = "";
        }

        updateTheBlockImages();

    }
    private void updateTheBlockImages()
    {
        updateGivenBlock(condition[0], block1Box.GetComponent<RawImage>());
        updateGivenBlock(condition[1], block2Box.GetComponent<RawImage>());
    }

    public void updateGivenBlock(int con, RawImage bBox)
    {

        switch (con)
        {
            case 2:
                bBox.texture = tapeBG;
                break;
            case 3:
                bBox.texture = glueBG;
                break;
            default:
                bBox.texture = baseBG;
                break;
        }

    }



    //A method to check if the card is playable due to manacost of the player and the card itself,
    // while also passing it down to block to check those isPlayables as well.
    public bool isPlayable()
    {
        for (int i = 0; i < blockArray.Length; i++)
        {
            Block block = blockArray[i];
            if (block != null && !block.isPlayable())
            {
                return false;
            }
        }
        return true;
    }

    //This method will be the main way to enact card effects, as it goes down the line to effect
    public void dealBlocks(GameObject Origin, GameObject Target)
    {

        Entity target = Target.GetComponent<Entity>();
        Entity origin = Origin.GetComponent<Entity>();

        for (int i = 0; i < blockArray.Length; i++)
        {
            if (blockArray[i] == null)
            {
                break;
            }
            Block block = blockArray[i];
            block.dealBlock(origin, target);
            // Debug.Log("Block dealt");
        }
    }
    

    //This method will remove any extra NON-Glued cards
    public void cleanUp()
    {
        if (blockArray[1].condition == 2)
        {
            blockArray[1].condition = 0;
        }
    }

    //This function will create the new "ripped card" Visually
    public void ripCard()
    {
        isRipped = true;
        //Play the animation here/create visuals for ripping
        
    }

    //Used for merging cards with glue or tape
    public Block addBlock(int cardId)
    {
        Block newBlock = new Block();
        List<int> rippedCard = CardDataBase.getBlockAtId(cardId);
        for (int i = 0; i < rippedCard[0]; i++)
        {
            newBlock.addEffect(rippedCard[i], rippedCard[i++]);
        }
        return newBlock;

    }

    public void createCombinedCard(int cardID)
    {
        blockArray[1] = addBlock(cardID);
        CardDataBase.allCards.Add(new List<int> {CardDataBase.allCards.Count, manaCost, cardVal, rarity, 2, 1,
            numOfEffects[0], effectType[0][0], effectVal[0][0], 2, numOfEffects[1], effectType[1][1], effectVal[1][1]}); //Check the 2D arrays
        CreateNewCard(CardDataBase.allCards.Count-1); //Check on this as this will likely not create the card how its supposed to 
        Debug.Log("New Card created");
        Destroy(this);
        // We destroy this card as we will be creating a new card to take its place
    }

    public void OnMouseDown()
    {
        if (!isShopItem) {
            cardManager.SetCard(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isRipped && Input.GetMouseButton(0))
        {
            ActualCard recivingCard = collision.gameObject.GetComponent<ActualCard>();
            if (recivingCard.condition[1] != 0)
            {
                Debug.Log("Cannot merge to this card");
            }
            else
            {
                recivingCard.createCombinedCard(cardID);
                Destroy(this);
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
