using System;
using System.Collections;
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

    public override IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        //TODO: Implement player death
        yield return new WaitForSeconds(0);
        
        statusUI.HideUI();
    }

    public override void OnMouseEnter()
    {
        statusUI.ActivateUI(this, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z));
    }
}