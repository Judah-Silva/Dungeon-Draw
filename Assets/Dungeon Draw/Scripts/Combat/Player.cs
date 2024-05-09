using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStats playerStats;
    
    public Slider manaBar;
    
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<Player>();
            }

            return _instance;
        }
        set => _instance = value;
    }

    private void Start()
    {
        Instance = this;
    }

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
        SetUpManaBar();
    }
    
    public void SetUpManaBar()
    {
        manaBar.maxValue = CardManager.Instance.maxMana;
        manaBar.value = CardManager.Instance.currentMana;
    }
    
    public void UpdateManaBar()
    {
        manaBar.value = CardManager.Instance.currentMana;
    }
    

    public override IEnumerator Die()
    {
        //TODO: Implement player death
        yield return new WaitForSeconds(0);
    }
}