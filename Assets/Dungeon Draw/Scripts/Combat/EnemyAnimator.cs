using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public ParticleSystem _particleSystem;
    public Sprite idleSprite;
    public Sprite attackSprite;
    public Sprite hitSprite;
    public Sprite deathSprite;
    
    public float moveDist = 1f;
    public float moveSpeed = 1f;
    public bool isIdle = true;
    private Vector3 initialPos;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(IdleAnimation());
    }

    IEnumerator IdleAnimation()
    {
        _spriteRenderer.sprite = idleSprite;

        while (isIdle)
        {
            while (transform.position.y < initialPos.y + moveDist)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                yield return null;
            }

            while (transform.position.y > initialPos.y)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    public void HitAnimation()
    {
        // isIdle = false;
        // _spriteRenderer.sprite = hitSprite;
        // transform.position = initialPos;
        StartCoroutine(HitDelay());
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = idleSprite;
    }

    public void AttackAnimation()
    {
        // isIdle = false;
        // _spriteRenderer.sprite = attackSprite;
        // transform.position = initialPos;
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        _spriteRenderer.sprite = attackSprite;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = idleSprite;
        // isIdle = true;
        // StartCoroutine(IdleAnimation());
    }

    public void DeathAnimation()
    {
        _spriteRenderer.sprite = deathSprite;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1f);
        CombatManager.Instance.RemoveEnemy(transform.parent.gameObject);
    }

    public void SpawnParticles()
    {
        _particleSystem.Play();
        // StartCoroutine(ShieldParticles());
    }
}
