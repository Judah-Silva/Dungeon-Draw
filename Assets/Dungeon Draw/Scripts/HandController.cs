using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public List<GameObject> CurrentHand; 
    public Transform RightBound;
    public Transform LeftBound;
    public int cardAmount;
    private static float cardWidth = .5f;

    void Start()
    {
        CurrentHand = new List<GameObject>();
    }

    private void Update()
    {
        
    }

    public void UpdateHand()
    {
        cardAmount = CurrentHand.Count;
        float offset = (RightBound.position.x - LeftBound.position.x) / cardAmount;
        for (int i = 0; i < cardAmount; i++) 
        {
            CurrentHand[i].transform.position = new Vector3(LeftBound.position.x+(offset*i)+cardWidth, LeftBound.position.y, LeftBound.position.z); 
            CurrentHand[i].transform.eulerAngles = new Vector3(LeftBound.eulerAngles.x, LeftBound.eulerAngles.y - 3, LeftBound.eulerAngles.z);
        
        }
    }
}
