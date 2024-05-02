using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyType type;
    
    public int goldValue;
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
        //CombatManager.Instance.RemoveEnemy(gameObject);
        //TODO : add gold to player
    }
}