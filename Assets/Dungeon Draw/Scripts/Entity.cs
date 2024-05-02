using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    public int maxHP;
    //[HideInInspector]
    public int currentHP;
    private int[] _entityStatusEffectArray = new int[10];
    
    public Slider healthBar;

    private CardManager _cardManager;

    public void SetUp()
    {
        currentHP = maxHP;
        Debug.Log("Setting up entity, currentHP: " + currentHP);
        _cardManager = CardManager.Instance;
        setUpEEA();
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar is not null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }
    }

    public void setUpEEA()
    {

        for (int i = 0; i < _entityStatusEffectArray.Length; i++)
        {
            _entityStatusEffectArray[i] = 0;
        }
    }

    private void OnMouseDown()
    {
        _cardManager.SetTarget(gameObject);
    }

    public void init(int health)
    {
        currentHP = health;
        maxHP = health;
    }

    public int getHP()
    {
        return currentHP;
    }

    public int getShield()
    {
        return _entityStatusEffectArray[0];
    }

    // Solely used by the effect class when get a modifier for dealing damage
    public int getDamageMod() {
        return _entityStatusEffectArray[3] - _entityStatusEffectArray[1];
    }

    public int takeDamage(int damage)
    {
        int remainingDamage = damage - getShield();
        // Debug.Log($"Taking {remainingDamage} damage");

        if (remainingDamage > 0)
        {
            currentHP -= remainingDamage;
            _entityStatusEffectArray[0] = 0;
        }
        else
        {
            _entityStatusEffectArray[0] -= damage;
        }

        Debug.Log($"{remainingDamage} damage taken to {gameObject.name}");
        if (currentHP <= 0)
        {
            Die();
            return 0;
        }
        UpdateHealthBar();
        return currentHP;
    }

    public int giveShield(int givenShield)
    {
        _entityStatusEffectArray[0] += givenShield;
        return _entityStatusEffectArray[0];
    }
    
    public void UpdateHealthBar()
    {
        if (healthBar is null) return;
        healthBar.value = currentHP;
    }
    
    public abstract void Die();
}
