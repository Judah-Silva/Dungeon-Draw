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
    
    public GameObject deathEffect;

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
        SetUpHealthBars();

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

    public override IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        CombatManager.Instance.RemoveEnemy(gameObject);
        PlayerStats.Gold += goldValue;
    }
}