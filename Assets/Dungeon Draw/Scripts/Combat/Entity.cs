using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Entity
{
    private bool _isInitialised = false;
    
    private int _blockingDamage = 0;
    private int _health;
    [HideInInspector]
    public int Health
    {
        get
        {
            if (!_isInitialised) _health = maxHealth;
            return _health;
        }
        set
        {
            _health = value;
            if (_health > maxHealth) _health = maxHealth;
        }
    }
    public int maxHealth;
    private List<Effect> _effects = new List<Effect>();
    
    public Entity(int health)
    {
        _health = health;
        maxHealth = health;
        _isInitialised = true;
    }
    
    public void ApplyCard(CardStats card)
    {
        foreach (Effect effect in card.Effects)
        {
            _effects.Add(effect.CreateEffect(this));
        }
    }
    
    public void AddEffect(Effect effect)
    {
        if (_effects.Contains(effect)) return;
        _effects.Add(effect);
    }
    
    public void RemoveEffect(Effect effect)
    {
        _effects.Remove(effect);
    }
    
    public void UpdateEffects()
    {
        for (int i = _effects.Count - 1; i >= 0; i--) //we need to iterate backwards because we're removing elements
        {
            Effect effect = _effects[i];
            effect.Update();
            if (effect.NbTurns <= 0)
                _effects.RemoveAt(i);
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (_blockingDamage > 0)
        {
            _blockingDamage -= damage;
            if (_blockingDamage < 0)
            {
                Health += _blockingDamage;
                _blockingDamage = 0;
            }
            return;
        }
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        CombatManager.Instance.enemies.Remove(this); //we can't do this because we're iterating over the list
        //TODO: Add death animation
    }

    public void AddBlockDamage(int damage)
    {
        _blockingDamage += damage;
    }
}
