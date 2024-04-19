using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Color c;
    private Renderer rend;
    private Animator anim;
    public float moveAmount = 1;
    public bool selected = false;

    public GameObject card;


    public int cardID;
    
    
    // Constructor to initialize card properties
    public Card(int id)
    {
	    cardID = id;
    }
    
    // Method to set card properties
    public void SetCardProperties(int id)
    {
	    cardID = id;
    }

    public int getCardID()
    {
	    return cardID;
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        RandomColor();
        //Debug.Log("Activating");
    }

    void RandomColor()
    {
        c.r = Random.Range(0f, 1f);
        c.g = Random.Range(0f, 1f);
        c.b = Random.Range(0f, 1f);
        //Debug.Log(c.r + " " + c.g +  " "+ c.b);
        rend.material.color = c;
    }

    void OnMouseDown()
    {	
	    PlayCard(card);
	}
        
	void OnMouseEnter ()
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
        
	void OnMouseExit ()
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
	
	public void PlayCard(GameObject card)
	{
		Debug.Log(cardID);
		
		// do card effect here
		
		Discard.DiscardPile.Add(cardID);
		// Debug.Log("discard = " + Discard.DiscardPile[0]);
		
		Destroy(card);
	}
}
