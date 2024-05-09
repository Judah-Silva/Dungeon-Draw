using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStats playerStats;
    
    public Slider manaBar;
    
    private static Player _instance;
    public static Player Instance
    
    public ParticleSystem hitParticles;

    public AudioClip swordAudio;
    public AudioClip dashAudio;
    public AudioClip shieldAudio;
    public AudioClip deathAudio;
    private AudioSource src;
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<Player>();
            }

            return _instance;
        }
        set => _instance = value;
    }

    private void Start()
    {
        Instance = this;
        src = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentHP < PlayerStats.CurrentHealth && !(currentHP <= 0))
        {
            GetComponent<Animator>().SetTrigger("Hit");
        }
        playerStats.UpdateHealth(currentHP);
    }

    public void PlayParticles()
    {
        hitParticles.Play();
    }

    public override void SetUp()
    {
        _cardManager = CardManager.Instance;
        setUpEEA();
        statusUI = GameObject.Find("Status UI").GetComponent<StatusUI>();
        maxHP = PlayerStats.MaxHealth;
        currentHP = PlayerStats.CurrentHealth;  
        SetUpManaBar();
    }
    
    public void SetUpManaBar()
    {
        manaBar.maxValue = CardManager.Instance.maxMana;
        manaBar.value = CardManager.Instance.currentMana;
    }
    
    public void UpdateManaBar()
    {
        manaBar.value = CardManager.Instance.currentMana;
    }
    

    public override IEnumerator Die()
    {
        src.clip = deathAudio;
        src.Play();
        GetComponent<Animator>().SetTrigger("Die");
    }

    public void OnDead()
    {
        CombatManager.Instance.ToGameOver();
    }
    
    /*---------for SFX----------*/

    public void DashSFX()
    {
        src.clip = dashAudio;
        src.Play();
    }
    
    public void AttackSFX()
    {
        src.clip = swordAudio;
        src.Play();
    }

    public void ShieldSFX()
    {
        src.clip = shieldAudio;
        src.Play();
    }
}