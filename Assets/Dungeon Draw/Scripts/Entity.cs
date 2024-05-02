using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
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

    public int getVul()
    {
        return entityStatusEffectArray[1];
    }

    // Solely used by the effect class when get a modifier for dealing damage
    public int getDamageMod() {
        return entityStatusEffectArray[2];
    }

    public int takeDamage(int damage)
    {
        int remainingDamage = damage - getShield() + getVul();
        // Debug.Log($"Taking {remainingDamage} damage");

        if (remainingDamage > 0)
        {
            currentHP -= remainingDamage;
            entityStatusEffectArray[0] = 0;

            Debug.Log($"{remainingDamage} damage has been dealt to {gameObject.name}");

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
        
        return currentHP;
    }

    public int giveShield(int givenShield)
    {
        entityStatusEffectArray[0] += givenShield;
        return entityStatusEffectArray[0];
    }

    public int giveVulnerable(int givenVulnerable)
    {

        Debug.Log($"{gameObject.name} has been given {givenVulnerable} vulnerability");

        entityStatusEffectArray[1] += givenVulnerable;
        return entityStatusEffectArray[1];
    }

    public int giveWeak(int givenWeakness)
    {

        Debug.Log($"{gameObject.name} has been given {givenWeakness} weakness");

        entityStatusEffectArray[2] += givenWeakness;
        return entityStatusEffectArray[2];
    }



    public void die()
    {
        CombatManager.Instance.RemoveEnemy(gameObject); //we can't do this because we're iterating over the list
        Destroy(gameObject);
        //TODO: Add death animation
    }
}
