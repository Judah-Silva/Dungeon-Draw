using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Discard : MonoBehaviour
{
    public static int currentSize;
    public int startSize = 0;

    public TextMeshProUGUI sizeText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSize = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        sizeText.text = currentSize.ToString();
    }
}
