using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    public int maxHP;
    //[HideInInspector]
    public int currentHP;
    private int prevHealth = 0;
    
    public StatusUI statusUI;

    private int[] entityStatusEffectArray = new int[10];

    public Slider grayHealthBar;
    public Slider redHealthBar;
    public Slider orangeHealthBar;

    [HideInInspector]
    public CardManager _cardManager;

    public abstract void SetUp();

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
    public int getDamageMod()
    {
        return entityStatusEffectArray[2];
    }

    public int TakeDamage(int damage)
    {
        int remainingDamage = damage - getShield() + getVul();
        // Debug.Log($"Taking {remainingDamage} damage");

        if (remainingDamage > 0)
        {
            prevHealth = currentHP;
            currentHP -= remainingDamage;
            entityStatusEffectArray[0] = 0;

            Debug.Log($"{remainingDamage} damage has been dealt to {gameObject.name}");
            UpdateHealthBar();
        }
        else
        {
            entityStatusEffectArray[0] -= damage;
        }

        if (currentHP <= 0)
        {
            StartCoroutine(Die());
            return 0;
        }
        

        return currentHP;
    }

    public int giveShield(int givenShield)
    {
        entityStatusEffectArray[0] += givenShield;
        UpdateHealthBar();
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

    public void UpdateHealthBar()
    {
        if (redHealthBar is null) return;
        redHealthBar.value = currentHP;
        if (orangeHealthBar is null) return;
        StartCoroutine(InterpolateHealth(orangeHealthBar, prevHealth, currentHP));
        if (grayHealthBar is null) return;
        grayHealthBar.value = getShield();
    }
    
    public void SetUpHealthBars()
    {
        if (redHealthBar is not null)
        {
            redHealthBar.maxValue = maxHP;
            redHealthBar.value = currentHP;
        }
        if (orangeHealthBar is not null)
        {
            orangeHealthBar.maxValue = maxHP;
            orangeHealthBar.value = currentHP;
        }
        if (grayHealthBar is not null)
        {
            grayHealthBar.maxValue = maxHP;
            grayHealthBar.value = 0;
        }
    }

    IEnumerator InterpolateHealth(Slider slider, float start, float end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < .5f)
        {
            float newHealth = Mathf.Lerp(start, end, elapsedTime / .5f);
            slider.value = newHealth;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        slider.value = end;
        prevHealth = currentHP;
    }

    public void OnMouseEnter()
    {
        Transform posToSpawn = gameObject.transform;
        statusUI.ActivateUI(posToSpawn);
    }

    public void OnMouseExit()
    {
        statusUI.HideUI();
    }
    
    public abstract IEnumerator Die();
    
}
