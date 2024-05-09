using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStats playerStats;
    
    public ParticleSystem hitParticles;

    public AudioClip swordAudio;
    public AudioClip dashAudio;
    public AudioClip shieldAudio;
    public AudioClip deathAudio;
    private AudioSource src;

    public TextMeshProUGUI manaText;
    public Slider manaBar;

    private static Player _instance;

    public static Player Instance
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
        Instance = this;
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
        manaText.text = $"Mana: {CardManager.Instance.getMana()} / {CardManager.Instance.maxMana}";
        manaBar.maxValue = CardManager.Instance.maxMana;
        manaBar.value = CardManager.Instance.getMana();
    }

    public void UpdateMana()
    {
        manaText.text = $"Mana: {CardManager.Instance.getMana()} / {CardManager.Instance.maxMana}";
        manaBar.value = CardManager.Instance.getMana();
    }
    

    public override IEnumerator Die()
    {
        src.clip = deathAudio;
        src.Play();
        GetComponent<Animator>().SetTrigger("Die");
        //TODO: Implement player death
        statusUI.HideUI();
        
        yield return null;
    }

    public override void OnMouseEnter()
    {
        statusUI.ActivateUI(this, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z));
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