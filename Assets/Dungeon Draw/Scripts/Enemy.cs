using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Enemy : MonoBehaviour
{
    public Entity entity;

    public String type;
    
    public int goldValue;
    
    public List<Block> blockList;

    private void Start()
    {
        blockList = new List<Block>();
        
        // Testing purposes
        blockList.Add(new Block().addEffect(0, 6));
        blockList.Add(new Block().addEffect(0, 6));
        blockList.Add(new Block().addEffect(1, 6));
        blockList.Add(new Block().addEffect(1, 6));
        blockList.Add(new Block().addEffect(0, 12));
    }

    public void Attack()
    {
        CombatManager combatManager = CombatManager.Instance;
        List<Effect> effectList = blockList[UnityEngine.Random.Range(0, blockList.Count)].effectList;
        foreach (Effect effect in effectList)
        {
            if (effect.GetEffectType() == 0)
            {
                effect.dealEffect(entity, combatManager.GetPlayerEntity());
            }
            else if (effect.GetEffectType() == 1)
            {
                Entity enemyEntity = combatManager.GetEnemyEntities()[UnityEngine.Random.Range(0, combatManager.GetEnemyEntities().Count)];
                effect.dealEffect(entity, enemyEntity);
            }
        }

    }
}