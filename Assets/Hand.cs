using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private int currentSize = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSize += 5;
    }

    public void PlayCard(GameObject card)
    {
        Discard.currentSize++;
        currentSize--;
        Destroy(card);
    }
}
