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

    private Animator bossAnimator;
    public AudioClip attackAudio;
    public AudioClip shieldAudio;
    public AudioClip deathAudio;
    private AudioSource src;
    
    private void Start()
    {
        bossAnimator = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
        
        _blockList = new List<Block>();
        
        // Testing purposes
        switch (gameObject.name)
        {
            case "Goblin":
                _blockList.Add(new Block().addEffect(0, 4));
                _blockList.Add(new Block().addEffect(0, 4));
                _blockList.Add(new Block().addEffect(1, 4));
                break;
            case "Watcher":
                _blockList.Add(new Block().addEffect(0, 8));
                _blockList.Add(new Block().addEffect(0, 8));
                _blockList.Add(new Block().addEffect(1, 6));
                _blockList.Add(new Block().addEffect(2, 3));
                _blockList.Add(new Block().addEffect(3, 3));
                break;
            case "DarkKnight":
                _blockList.Add(new Block().addEffect(0, 12));
                _blockList.Add(new Block().addEffect(0, 10));
                _blockList.Add(new Block().addEffect(0, 10));
                _blockList.Add(new Block().addEffect(1, 8));
                _blockList.Add(new Block().addEffect(1, 8));
                _blockList.Add(new Block().addEffect(6, 10));
                break;
            case "Boss":
                // probably change this
                _blockList.Add(new Block().addEffect(0, 10));
                _blockList.Add(new Block().addEffect(0, 15));
                _blockList.Add(new Block().addEffect(0, 15));
                _blockList.Add(new Block().addEffect(0, 25));
                _blockList.Add(new Block().addEffect(6, 15));
                _blockList.Add(new Block().addEffect(1, 15));
                _blockList.Add(new Block().addEffect(1, 15));
                _blockList.Add(new Block().addEffect(1, 10));
                _blockList.Add(new Block().addEffect(2, 6));
                _blockList.Add(new Block().addEffect(2, 6));
                _blockList.Add(new Block().addEffect(3, 6));
                _blockList.Add(new Block().addEffect(3, 6));
                _blockList.Add(new Block().addEffect(4, 6));
                Block bigMove = new Block().addEffect(6, 10);
                bigMove.addEffect(2, 6);
                _blockList.Add(bigMove);
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
            if (effect.GetEffectType() == 1)
            {
                PlaySFX(shieldAudio);
                if (enemyAnimator != null)
                {
                    enemyAnimator._particleSystem.Play();
                }
                Entity enemyEntity = combatManager.GetEnemyEntities()[UnityEngine.Random.Range(0, combatManager.GetEnemyEntities().Count)];
                effect.dealEffect(this, enemyEntity);
            }
            else if (effect.GetEffectType() == 5)
            {
                PlaySFX(shieldAudio);
                if (enemyAnimator != null)
                {
                    enemyAnimator._particleSystem.Play();
                }
                Entity enemyEntity = combatManager.GetEnemyEntities()[UnityEngine.Random.Range(0, combatManager.GetEnemyEntities().Count)];
                effect.dealEffect(this, enemyEntity);
            }
            else
            {
                PlaySFX(attackAudio);
                if (enemyAnimator != null)
                {
                    enemyAnimator.AttackAnimation();
                }
                else
                {
                    bossAnimator.SetTrigger("Attack");
                }
                effect.dealEffect(this, combatManager.GetPlayerEntity());
            }
        }
        
    }

    public override IEnumerator Die()
    {
        PlaySFX(deathAudio);
        PlayerStats.Coins += goldValue;
        CombatManager.Instance.earnedGold += goldValue;
        if (enemyAnimator != null)
        {
            enemyAnimator.DeathAnimation();
        }
        else
        {
            bossAnimator.SetTrigger("Die");
        }
        
        // CombatManager.Instance.RemoveEnemy(gameObject);

        statusUI.HideUI();
        yield return null;
    }

    public void Animate()
    {
        if (enemyAnimator != null)
        {
            enemyAnimator.HitAnimation();
        }
        else
        {
            bossAnimator.SetTrigger("Hit");
        }
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