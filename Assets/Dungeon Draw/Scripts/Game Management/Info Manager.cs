using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI gold;
    public GameObject relicHolder;
    public GameObject relicUIGameObject;

    private void Start()
    {
        for (int i =0; i<PlayerStats.Relics.Count; i++)
        {
            int objId = i;
            GameObject g = Instantiate(relicUIGameObject, relicHolder.transform);
            g.GetComponent<Image>().sprite = PlayerStats.Relics[i].art;
            EventTrigger t = g.GetComponent<EventTrigger>();
            EventTrigger.Entry ent = new EventTrigger.Entry();
            ent.eventID = EventTriggerType.PointerEnter;
            ent.callback.AddListener((data) => { RelicHovered(objId); });
            t.triggers.Add(ent);
            EventTrigger.Entry ent2 = new EventTrigger.Entry();
            ent2.eventID = EventTriggerType.PointerExit;
            ent2.callback.AddListener((data) => { RelicUnHovered(objId); });
            t.triggers.Add(ent2);
        }
    }

    private void Update()
    {
        health.text = $"{PlayerStats.CurrentHealth}/{PlayerStats.MaxHealth} .";
        gold.text = $"{PlayerStats.Coins}";
    }

    public void updateRelics(Relic r) //used when a relic is first gotten
    {
        int objId = PlayerStats.Relics.Count - 1;
        GameObject g = Instantiate(relicUIGameObject, relicHolder.transform);
        g.GetComponent<Image>().sprite = r.art;
         EventTrigger t = g.GetComponent<EventTrigger>();
        EventTrigger.Entry ent = new EventTrigger.Entry();
        
        ent.eventID = EventTriggerType.PointerEnter;
        ent.callback.AddListener((data) => { RelicHovered(objId); });
        t.triggers.Add(ent);
        EventTrigger.Entry ent2 = new EventTrigger.Entry();
        ent2.eventID = EventTriggerType.PointerExit;
        ent2.callback.AddListener((data) => { RelicUnHovered(objId); });
        t.triggers.Add(ent2);
    }
    
    public void RelicHovered(int obj)
    {
        // Debug.Log("Relic : " + obj + " Hovered");
        this.transform.GetChild(2).GetChild(obj).GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(2).GetChild(obj).GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = ("<b>" +PlayerStats.Relics[obj].name + "</b>\n" + PlayerStats.Relics[obj].description);
    }

    public void RelicUnHovered(int obj)
    {
        // Debug.Log("Unhovered : " + obj);
        this.transform.GetChild(2).GetChild(obj).GetChild(0).gameObject.SetActive(false);
    }
}
