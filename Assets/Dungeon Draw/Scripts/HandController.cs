using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public GameObject handArea;
    public float spacing = 1f;
    
    public static List<ActualCard> hand = new List<ActualCard>();

    private Deck _deck;
    private BoxCollider handAreaCollider;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _deck = GetComponent<Deck>();
        // Debug.Log(_deck == null);
        handAreaCollider = handArea.GetComponent<BoxCollider>();
    }

    void AddCardToHand()
    {
        GameObject newCard = _deck.DrawCard();
        
        Vector3 size = handAreaCollider.bounds.size;
        Vector3 center = handAreaCollider.transform.position;
        // Debug.Log(center);

        hand.Add(newCard.GetComponent<ActualCard>());
        if (hand.Count == 0)
        {
            newCard.transform.position = new Vector3(center.x, center.y, center.z);
        }
        else
        {
            float width = size.x;
            float cardWidth = newCard.transform.localScale.x;
            float spaceTaken = (hand.Count * cardWidth) + ((hand.Count - 1) * spacing);
            float leftStart = (-spaceTaken / 2) + cardWidth / 2;

            foreach (ActualCard c in hand)
            {
                c.transform.position = new Vector3(leftStart, center.y, center.z);
                leftStart += cardWidth + spacing;
            }
        }
        newCard.SetActive(true);
    }

    public IEnumerator DrawHand()
    {
        Debug.Log("Hand drawing");
        while (hand.Count < PlayerStats.HandSize && _deck.deckSize > 0)
        {
            AddCardToHand();
            yield return new WaitForSeconds(0.25f);
        }
    }

    public static void RemoveCard(ActualCard card)
    {
        hand.Remove(card);
    }

    // void ReadjustCards(GameObject card)
    // {
    //     Vector3 size = handAreaCollider.bounds.size;
    //     Vector3 center = handAreaCollider.center;
    //
    //     hand.Add(card.GetComponent<Card>());
    //     if (hand.Count == 0)
    //     {
    //         card.transform.position = new Vector3(center.x, center.y, 0);
    //     }
    //     else
    //     {
    //         float width = size.x;
    //         float cardWidth = card.transform.localScale.x;
    //         float spaceTaken = (hand.Count * cardWidth) + ((hand.Count - 1) * spacing);
    //         float leftStart = (-spaceTaken / 2) + cardWidth / 2;
    //
    //         foreach (Card c in hand)
    //         {
    //             c.transform.position = new Vector3(leftStart, c.transform.position.y, 0);
    //             leftStart += cardWidth + spacing;
    //         }
    //     }
    // }
    
}
