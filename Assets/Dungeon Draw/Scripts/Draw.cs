using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public static int pileSize;

    public TextMeshProUGUI sizeText;
    
    // Start is called before the first frame update
    void Start()
    {
        pileSize = PlayerStats.DeckSize - Hand.drawAmount;
        Debug.Log(PlayerStats.DeckSize);
        sizeText.text = pileSize.ToString();
    }

    public static void DrawFromPile()
    {
        if (pileSize == 0)
        {
            Debug.Log("deck empty");
        }
        else if (pileSize < Hand.drawAmount)
        {
            Hand.DrawCard(pileSize);
            pileSize -= pileSize;
        }
        else
        {
            Hand.DrawCard(Hand.drawAmount);
            pileSize -= Hand.drawAmount;
        }
    }

    private void Update()
    {
        sizeText.text = pileSize.ToString();
    }
}
