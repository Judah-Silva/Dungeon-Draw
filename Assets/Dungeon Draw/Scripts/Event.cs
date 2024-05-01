using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum Actions //FILL THIS WITH MORE ACTIONS
{
    Leave, //Where change scene will instantly take you to the next scene, this will give the user a response. 
    LoseGold,
    GainGold,
    GainHealth,
    LoseHealth,
    GainCardById,
    GainRelicById,
    GainGlue,
    GainTape,
    ChangeScene,
}

// [System.Serializable]
// public struct ButtonAction
// {
//     public Actions actionType;
//     public string stringVal;
//     public int intVal;
// }

[System.Serializable]
public struct buttons //NOTE IF YOU HAVE MULTIPLE ACTIONS ON A BUTTON, MAKE SURE THAT BOTH intActs AND stringActs ARE THE SAME LENGTH AS THE AMOUNT OF ACTIONS
{ //ALSO MAKE SURE THAT A 'ChangeScene' ACTION OCCURS LAST
    public string Text;
    [TextArea(5, 5)]
    public string postButtonPressedText;
    public Actions[] actions;
    public string[] stringActs;
    public int[] intActs;
} 

[System.Serializable]
public class eventClass
{
    public string eventTitle;
    
    [TextArea(5, 5)]
    public string eventText;

    public buttons[] button; 
    public Texture image;
}

public class Event : MonoBehaviour
{
    public GameObject[] ButtonGO;
    public TMP_Text TitleTextBox;
    public TMP_Text DescriptionBox;
    public string mapSceneName;
    
    public List<eventClass> events;
  

    [Header("EventId: -1 For random events | 0->n for specific event")]
    public int eventId = -1; //SET TO -1 FOR RANDOM EVENTS || CHANGE VALUE TO TEST SPECIFIC EVENT (IN INSPECTOR)

    
    void Start()
    {
       ButtonGO[0].SetActive(false);ButtonGO[1].SetActive(false);ButtonGO[2].SetActive(false); // Turns buttons off
        if (eventId == -1)
            eventId = Random.Range(0, events.Count); // selects random event
        Debug.Log(eventId);
        
        //--------------------------------------- Sets up the scene based on event 'id'
        TitleTextBox.text = events[eventId].eventTitle;
        DescriptionBox.text = events[eventId].eventText;
        for (int i = 0; i < events[eventId].button.Length; i++) //Turns on used buttons and sets button text (3 Button limit atm)
        {
            ButtonGO[i].SetActive(true);
            ButtonGO[i].GetComponentInChildren<TMP_Text>().text = events[eventId].button[i].Text;
        } 
        if (events[eventId].image!=null)  
        {
            GameObject.Find("EventArt").GetComponent<RawImage>().texture = events[eventId].image;
        }
        //------------------------------------------ 
    }


    public void ButtonPressed(int buttonId)
    {
        if (ButtonGO[0].GetComponentInChildren<TMP_Text>().text == "Back To Map") 
        {
            SceneManager.LoadScene(mapSceneName);// This only occurs after this function has happened once  |||  unless you set a button to this name but that shouldn't be necessary 
            return;
        }
        int actionLength = events[eventId].button[buttonId].actions.Length;
        buttons but = events[eventId].button[buttonId];
        for (int i = 0; i < actionLength; i++)
        {
            switch (but.actions[i])
            {
               
               case Actions.LoseGold:
                   Debug.Log("LoseGold activated");
                   PlayerStats.Coins -= but.intActs[i];
                   break;
               case Actions.GainGold:
                   Debug.Log("GainGold activated");
                   PlayerStats.Coins += but.intActs[i];
                   break;
               case Actions.LoseHealth:
                   Debug.Log("LoseHealth activated");
                   GameObject.Find("Player").GetComponent<Entity>().currentHP -= but.intActs[i];
                   break;
               case Actions.GainHealth:
                   Debug.Log("GainHealth activated");
                   GameObject.Find("Player").GetComponent<Entity>().currentHP += but.intActs[i];
                   break;
               case Actions.GainCardById :
                   Debug.Log("GainCardById  activated");
                   CardDataBase.heldCards.Add(CardDataBase.getCard(but.intActs[i]));
                   break;
               case Actions.GainRelicById :
                  Debug.Log("GainRelicById  activated");
                  //relicDB.heldRelics.add(relicDB.getRelic(but.intActs[i]));
                  break;
               case Actions.GainGlue :
                  Debug.Log("GainGlue activated");
                  PlayerStats.Glue += but.intActs[i];
                  break;
               case Actions.GainTape :
                  Debug.Log("GainTape  activated");
                  PlayerStats.Tape += but.intActs[i];
                  break;
               case Actions.Leave:
                   Debug.Log("Leave button pressed");
                   //Used for getting the player to the next text display without affecting any values
                   break;
               case Actions.ChangeScene:
                   Debug.Log("ChangeScene activated");
                   SceneManager.LoadScene(but.stringActs[i]);
                   break;
               
            } 
            DescriptionBox.text = events[eventId].button[buttonId].postButtonPressedText; //Displays postButtonPressedText var
            ButtonGO[1].SetActive(false);ButtonGO[2].SetActive(false); // Turns top buttons off
            ButtonGO[0].GetComponentInChildren<TMP_Text>().text = "Back To Map"; //Changes to a leave button
        }
    }

    

}


