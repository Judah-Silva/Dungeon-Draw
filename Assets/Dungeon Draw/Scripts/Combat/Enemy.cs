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

    public EnemyAnimator enemyAnimator;

    public AudioClip attackAudio;
    public AudioClip shieldAudio;
    public AudioClip deathAudio;
    private AudioSource src;
    
    private void Start()
    {
        src = GetComponent<AudioSource>();
        
        _blockList = new List<Block>();
        
        // Testing purposes
        switch (gameObject.name)
        {
            case "Goblin":
                _blockList.Add(new Block().addEffect(0, 3));
                _blockList.Add(new Block().addEffect(0, 3));
                _blockList.Add(new Block().addEffect(1, 3));
                break;
            case "Watcher":
                _blockList.Add(new Block().addEffect(0, 4));
                _blockList.Add(new Block().addEffect(0, 4));
                _blockList.Add(new Block().addEffect(1, 4));
                _blockList.Add(new Block().addEffect(2, 4));
                _blockList.Add(new Block().addEffect(3, 4));
                break;
            case "DarkKnight":
                //_blockList.Add(new Block().addEffect(0, 5));
                //_blockList.Add(new Block().addEffect(0, 5));
                _blockList.Add(new Block().addEffect(1, 5));
                _blockList.Add(new Block().addEffect(1, 5));
                _blockList.Add(new Block().addEffect(1, 5));
                break;
            case "Boss":
                _blockList.Add(new Block().addEffect(0, 10));
                _blockList.Add(new Block().addEffect(0, 15));
                break;
            default:
                _blockList.Add(new Block().addEffect(0, 6));
                _blockList.Add(new Block().addEffect(1, 6));
                break;
        }
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
                PlaySFX(attackAudio);
                enemyAnimator.AttackAnimation();
                effect.dealEffect(this, combatManager.GetPlayerEntity());
            }
            else if (effect.GetEffectType() == 1)
            {
                PlaySFX(shieldAudio);
                enemyAnimator._particleSystem.Play();
                Entity enemyEntity = combatManager.GetEnemyEntities()[UnityEngine.Random.Range(0, combatManager.GetEnemyEntities().Count)];
                effect.dealEffect(this, enemyEntity);
            }
        }
        
    }

    public override IEnumerator Die()
    {
        PlaySFX(deathAudio);
        PlayerStats.Coins += goldValue;
        CombatManager.Instance.earnedGold += goldValue;
        enemyAnimator.DeathAnimation();
        // CombatManager.Instance.RemoveEnemy(gameObject);

        statusUI.HideUI();
        yield return null;
    }

    public void Animate()
    {
        enemyAnimator.HitAnimation();
    }

    public void PlaySFX(AudioClip sfx)
    {
        src.clip = sfx;
        src.Play();
    }

    public override void OnMouseEnter() {
        statusUI.ActivateUI(this, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z));
    }
}