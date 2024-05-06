using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI gold;
    public GameObject relicHolder;
    public GameObject relicUIGameObject;

    private void Start()
    {
        foreach (Relic r in PlayerStats.Relics) //Makes sure UI is filled with users relics each time UI is loaded
        {
            GameObject g = Instantiate(relicUIGameObject, relicHolder.transform);
            g.GetComponent<Image>().sprite = r.art; 
        }
    }

    private void Update()
    {
        health.text = $"{PlayerStats.CurrentHealth}/{PlayerStats.MaxHealth} .";
        gold.text = $"{PlayerStats.Coins}";
    }

    public void updateRelics(Relic r) //used when a relic is first gotten
    {
        GameObject g = Instantiate(relicUIGameObject, relicHolder.transform);
        g.GetComponent<Image>().sprite = r.art;
    }
}
