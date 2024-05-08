using System;
using UnityEngine;

public class Player : Entity
{
    public PlayerStats playerStats;
    private void Update()
    {
        if (currentHP < PlayerStats.CurrentHealth)
        {
            GetComponent<Animator>().SetTrigger("Hit");
        }
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

    public override void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        //TODO: Implement player death
    }
}