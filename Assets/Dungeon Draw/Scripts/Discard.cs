using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    public static List<int> DiscardPile;
    
    // Start is called before the first frame update
    void Start()
    {
        DiscardPile = new List<int>();
    }

    private void OnMouseDown()
    {
        int size = DiscardPile.Count;
        for (int i = 0; i < size; i++)
        {
            Debug.Log("Discard at i = " + DiscardPile[i]);
        }
    }
}
