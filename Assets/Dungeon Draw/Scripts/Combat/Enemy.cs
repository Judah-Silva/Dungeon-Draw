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
    }

    public void Attack()
    {
        CombatManager combatManager = CombatManager.Instance;
        List<Effect> effectList = blockList[UnityEngine.Random.Range(0, blockList.Count)].effectList;
        foreach (Effect effect in effectList)
        {
            if (effect.GetEffectType() == 0)
            {
                effect.dealEffect(entity, combatManager.playerEntity);
            }
            else if (effect.GetEffectType() == 1)
            {
                Entity enemyEntity = combatManager.GetEnemies()[UnityEngine.Random.Range(0, combatManager.GetEnemies().Count)];
                effect.dealEffect(entity, enemyEntity);
            }
        }

    }
}
