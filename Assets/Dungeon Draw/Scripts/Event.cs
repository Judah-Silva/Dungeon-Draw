using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class eventClass
{
    public string eventTitle;
    
    [TextArea(10, 10)]
    public string eventText;
    public string[] buttonTexts;
    public Texture image;
}

public class Event : MonoBehaviour
{
    public GameObject[] buttons;
    public eventClass[] events;
    public TMP_Text TitleTextBox;

    public TMP_Text DescriptionBox;

    
    void Start()
    {
        buttons[0].SetActive(false);buttons[1].SetActive(false);buttons[2].SetActive(false); // Turns buttons off
        int eventId = Random.Range(0, events.Length); // selects random event
        Debug.Log(eventId);
        
        //--------------------------------------- Sets up the scene based on event 'id'
        TitleTextBox.text = events[eventId].eventTitle;
        DescriptionBox.text = events[eventId].eventText;
        for (int i = 0; i < events[eventId].buttonTexts.Length; i++) //Turns on used buttons and sets button text (3 Button limit atm)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TMP_Text>().text = events[eventId].buttonTexts[i];
        } 
        if (events[eventId].image!=null)  
        {
            GameObject.Find("EventArt").GetComponent<RawImage>().texture = events[eventId].image;
        }
        //------------------------------------------ 
    }


    public void ButtonAPressed()
    {
    }

    public void ButtonBPressed()
    {
        
    }

    public void ButtonCPressed()
    {
        
    }

}


