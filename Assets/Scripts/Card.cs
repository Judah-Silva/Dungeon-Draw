using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Color c;
    private Renderer rend;
    public float moveAmount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        RandomColor();
        //Debug.Log("Activating");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomColor()
    {
        c.r = Random.Range(0f, 1f);
        c.g = Random.Range(0f, 1f);
        c.b = Random.Range(0f, 1f);
        //Debug.Log(c.r + " " + c.g +  " "+ c.b);
        rend.material.color = c;
    }
    void OnMouseDown ()
        	{
		        
    	    }
        
        void OnMouseEnter ()
        {
	        c.r -= .1f;
	        c.g -= .1f;
	        c.b -= .1f;
    		    rend.material.color = c;
		        for (float i = moveAmount; i > 0; i-=.01f)
		        {
			        //transform.Translate(transform.position.x, transform.position.y + i, transform.position.z);
			        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + i,
				        transform.localPosition.z);
		        }
        }
        
        	void OnMouseExit ()
        	{
		        c.r += .1f;
				c.g += .1f;
				c.b += .1f;
    		    rend.material.color = c;
		        for (float i = moveAmount; i > 0; i-=.01f)
		        {
			        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - i,
                    				        transform.localPosition.z);
		        }
            }
    public static void PlayCard(GameObject card)
    {
        Discard.currentSize++;
        Hand.currentSize--;
        
        // do card effect
        
        Destroy(card);

        if (Hand.currentSize == 0)
        {
            Draw.DrawFromPile();
        }
    }
}
