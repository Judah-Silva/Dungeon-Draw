using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static GameObject cardPrefab;
    public GameObject card;
    public static Transform handTransform;
    public Transform hand;

    public static int currentSize;

    public static int drawAmount;
    public int drawNum = 5; // how many cards drawn per turn
    
    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = card;
        handTransform = hand;

        drawAmount = drawNum;
        
        currentSize = 0;
        DrawCard(drawAmount);
    }
    

    public static void DrawCard(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject card = Instantiate(cardPrefab, handTransform);
            currentSize++;
        }
    }
}
