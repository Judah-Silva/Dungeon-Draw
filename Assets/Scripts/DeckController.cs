using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour
{
	private Renderer rend;
	private MeshRenderer meshRend;

	public Color hoverColor;

	public Color startColor;

	public HandController hand;
	public Stack<GameObject> deck;
	public GameObject card;
	public float cardSize = .2f;
	public int startingCardAmountInDeck = 12;
	
    // Start is called before the first frame update
    void Start()
    {
	    rend = GetComponent<Renderer>();
	    meshRend = GetComponent<MeshRenderer>();
	    deck = new Stack<GameObject>();
	    for (int i = 0; i < startingCardAmountInDeck; i++)
	    {
		    
		    deck.Push((GameObject)Instantiate(card, transform.position, transform.rotation));
		    Debug.Log(deck.Count);
		    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, cardSize*deck.Count);
	    }
    }

    // Update is called once per frame
    void Update()
    {
	    if (deck.Count == 0)
	    {
		    meshRend.enabled = false;
	    }
	    else
	    {
		    //meshRend.enabled = true;
	    }
	    
    }
    
    void OnMouseDown ()
    	{
		    // if (EventSystem.current.IsPointerOverGameObject())
    		// 	return;
		    
		    if (deck.Count != 0)
		    {
			    hand.CurrentHand.Add(deck.Peek());
			    hand.UpdateHand();
			    deck.Pop();
			    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, cardSize*deck.Count);
		    }

		    Debug.Log(deck.Count);
	    }
    
    void OnMouseEnter ()
    	{
    		// if (EventSystem.current.IsPointerOverGameObject())
    		// 	return;
		    rend.material.color = hoverColor;
    	}
    
    	void OnMouseExit ()
    	{
    		rend.material.color = startColor;
        }
}
