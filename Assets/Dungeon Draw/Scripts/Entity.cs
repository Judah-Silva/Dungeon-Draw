using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int[] entityEffectArray = new int[10];

    public void Start()
    {
        setUpEEA();
    }

    public void setUpEEA()
    {

        for (int i = 0; i < entityEffectArray.Length; i++)
        {
            entityEffectArray[i] = 0;
        }

    }
    
    public Entity(int health)
    {
        currentHP = health;
        maxHP = health;
    }

    public int getHP()
    {
        return currentHP;
    }

    public int getBlock()
    {
        return entityEffectArray[0];
    }

    public int takeDamage(int damage)
    {

        int remainingDamage = getBlock() - damage;

        if (remainingDamage > 0)
        {
            currentHP -= remainingDamage;
            entityEffectArray[0] = 0;
        }
        else
        {
            entityEffectArray[0] -= damage;
        }

        if (currentHP <= 0)
        {
            die();
        }
        return currentHP;
    }

    public int giveBlock(int givenBlock)
    {
        entityEffectArray[0] += givenBlock;
        return entityEffectArray[0];
    }
    
    public void die()
    {
        CombatManager.Instance.enemies.Remove(this); //we can't do this because we're iterating over the list
        //TODO: Add death animation
    }
}
