using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    public int maxHP;
    [HideInInspector] public int currentHP;
    public int[] entityStatusEffectArray = new int[10];

    private CardManager _cardManager;

    public void Start()
    {
        _cardManager = CardManager.Instance;
        setUpEEA();
    }

    public void setUpEEA()
    {

        for (int i = 0; i < entityStatusEffectArray.Length; i++)
        {
            entityStatusEffectArray[i] = 0;
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
        return entityStatusEffectArray[0];
    }

    // Solely used by the effect class when get a modifier for dealing damage
    public int getDamageMod() {
        return entityStatusEffectArray[3] - entityStatusEffectArray[1];
    }

    public int takeDamage(int damage)
    {
        int remainingDamage = damage - getShield();
        Debug.Log($"Taking {remainingDamage} damage");

        if (remainingDamage > 0)
        {
            currentHP -= remainingDamage;
            entityStatusEffectArray[0] = 0;
        }
        else
        {
            entityStatusEffectArray[0] -= damage;
        }

        if (currentHP <= 0)
        {
            die();
            return 0;
        }
        
        Debug.Log($"{currentHP} hp remaining");
        return currentHP;
    }

    public int giveShield(int givenShield)
    {
        entityStatusEffectArray[0] += givenShield;
        return entityStatusEffectArray[0];
    }
    
    public void die()
    {
        CombatManager.Instance.enemies.Remove(this); //we can't do this because we're iterating over the list
        Destroy(gameObject);
        //TODO: Add death animation
    }
}
