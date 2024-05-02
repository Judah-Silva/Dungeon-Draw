using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI gold;

    // public PlayerStats playerStats;

    private void Update()
    {
        health.text = $"{PlayerStats.CurrentHealth}/{PlayerStats.MaxHealth} .";
        gold.text = $"{PlayerStats.Coins}";
    }
}
