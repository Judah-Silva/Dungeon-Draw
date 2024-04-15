using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public int currentSize = 10;

    public TextMeshProUGUI sizeText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSize = PlayerStats.DeckSize;
        sizeText.text = currentSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
