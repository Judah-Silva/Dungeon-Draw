using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerStats playerStats;
    private void Update()
    {
        playerStats.UpdateHealth(currentHP);
    }

    public override void SetUp()
    {
        _cardManager = CardManager.Instance;
        setUpEEA();
        statusUI = GameObject.Find("Status UI").GetComponent<StatusUI>();
        maxHP = PlayerStats.MaxHealth;
        currentHP = PlayerStats.CurrentHealth;  
    }

    public override IEnumerator Die()
    {
        //TODO: Implement player death
        yield return new WaitForSeconds(0);
    }
}