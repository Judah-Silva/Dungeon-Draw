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
    public TextMeshProUGUI glueText;
    public TextMeshProUGUI tapeText;
    public Slider tapeBar;
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
            this.transform.GetChild(2).GetChild(objId).GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = ("<b>" +PlayerStats.Relics[objId].name + "</b>\n" + PlayerStats.Relics[objId].description);
        }

        PlayerStats.OnTapeChange += PlayerStatsTapeChange;
        PlayerStats.OnGlueChange += PlayerStatsOnGlueChange;
    }
    
    private void Update()
    {
        health.text = $"{PlayerStats.CurrentHealth}/{PlayerStats.MaxHealth} .";
        gold.text = $"{PlayerStats.Coins}";
    }

    private void PlayerStatsOnGlueChange()
    {
        glueText.text = $"Glue: {PlayerStats.Glue}";
    }
    
    private void  PlayerStatsTapeChange()
    {
        StartCoroutine(InterpolateTape(tapeBar.value, PlayerStats.Tape - Mathf.Floor(PlayerStats.Tape)));
        tapeText.text = $"Tape: {Mathf.Floor(PlayerStats.Tape)} / {PlayerStats.MaxTape}";
    }

    IEnumerator InterpolateTape(float start, float end)
    {
        float tempEnd = end;
        if (end < start)
        {
            tempEnd = 1f;
        }

        float elapsedTime = 0f;
        while (elapsedTime < .5f)
        {
            float newTape = Mathf.Lerp(start, tempEnd, elapsedTime / .5f);
            tapeBar.value = newTape;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tapeBar.value = end;
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
        
        this.transform.GetChild(2).GetChild(objId).GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = ("<b>" +PlayerStats.Relics[objId].name + "</b>\n" + PlayerStats.Relics[objId].description);
    }

    public void removeRelicUI(int objId)
    {
       // this.transform.GetChild(2).GetChild(objId).GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = ("<b>" +PlayerStats.Relics[objId].name + "</b>\n" + "\nThis Relic has been Used up");
       this.transform.GetChild(2).GetChild(objId).gameObject.SetActive(false);
            
    }
    
    public void RelicHovered(int obj)
    {
        // Debug.Log("Relic : " + obj + " Hovered");
        this.transform.GetChild(2).GetChild(obj).GetChild(0).gameObject.SetActive(true);
    }

    public void RelicUnHovered(int obj)
    {
        // Debug.Log("Unhovered : " + obj);
        this.transform.GetChild(2).GetChild(obj).GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerStats.OnTapeChange -= PlayerStatsTapeChange;
        PlayerStats.OnGlueChange -= PlayerStatsOnGlueChange;
    }
}
