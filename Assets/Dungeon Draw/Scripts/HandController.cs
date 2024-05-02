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
            RearrangeCards(newCard.transform.localScale.x);
        }
        newCard.SetActive(true);
    }

    private void RearrangeCards(float cardWidth)
    {
        Vector3 size = handAreaCollider.bounds.size;
        Vector3 center = handAreaCollider.transform.position;

        float width = size.x;
        float spaceTaken = (hand.Count * cardWidth) + ((hand.Count - 1) * spacing);
        float leftStart = (-spaceTaken / 2) + cardWidth / 2;

        foreach (ActualCard c in hand)
        {
            c.transform.position = new Vector3(leftStart, center.y, center.z);
            c.originalPosition = c.transform.position;
            leftStart += cardWidth + spacing;
        }
    }

    public IEnumerator DrawHand()
    {
        // Debug.Log("Hand drawing");
        // Debug.Log(_deck.deckSize);
        while (hand.Count < PlayerStats.HandSize && _deck.deckSize > 0)
        {
            AddCardToHand();
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void NewHand()
    {
        StartCoroutine(DrawHand());
    }

    public void ClearHand()
    {
        foreach (ActualCard card in hand)
        {
            Destroy(card.gameObject);
        }
        hand.Clear();
    }

    public List<ActualCard> GetHand()
    {
        return hand;
    }

    public void RemoveCard(ActualCard card)
    {
        hand.Remove(card);
        float cardWidth = card.transform.localScale.x;
        Destroy(card.gameObject);
        RearrangeCards(cardWidth);
    }


}