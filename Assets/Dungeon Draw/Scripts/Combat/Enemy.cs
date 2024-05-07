using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    public int goldValue;
    public bool isBoss = false;
    private List<Block> _blockList;

    private void Start()
    {
        _blockList = new List<Block>();
        
        // Testing purposes
        _blockList.Add(new Block().addEffect(0, 6));
        _blockList.Add(new Block().addEffect(0, 6));
        _blockList.Add(new Block().addEffect(1, 6));
        _blockList.Add(new Block().addEffect(1, 6));
        _blockList.Add(new Block().addEffect(0, 12));
    }
    
    public override void SetUp()
    {
        _cardManager = CardManager.Instance;
        setUpEEA();
        statusUI = GameObject.Find("Status UI").GetComponent<StatusUI>();
        currentHP = maxHP;
        Debug.Log("Setting up entity, currentHP: " + currentHP);
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar is not null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }
    }

    public void Attack()
    {
        CombatManager combatManager = CombatManager.Instance;
        List<Effect> effectList = _blockList[UnityEngine.Random.Range(0, _blockList.Count)].effectList;
        foreach (Effect effect in effectList)
        {
            if (effect.GetEffectType() == 0)
            {
                effect.dealEffect(this, combatManager.GetPlayerEntity());
            }
            else if (effect.GetEffectType() == 1)
            {
                Entity enemyEntity = combatManager.GetEnemyEntities()[UnityEngine.Random.Range(0, combatManager.GetEnemyEntities().Count)];
                effect.dealEffect(this, enemyEntity);
            }
        }
        
    }

    public override void Die()
    {
        CombatManager.Instance.RemoveEnemy(gameObject);
        PlayerStats.Gold += goldValue;
    }
}