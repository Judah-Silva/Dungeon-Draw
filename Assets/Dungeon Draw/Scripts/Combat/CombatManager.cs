using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<Entity> enemies = new List<Entity>();

    public GameObject resultsWindow;
    
    /*
    Use StartFight() to start the fight
    It waits for the player to play first if PlayerPlayFirst is true
    Use the end turn button to end the player turn (EndTurn())
    Play the enemy turn with the coroutine EnemyTurn() (TODO: Replace with enemy turn logic)
    And wait for the player to play again
     */

    public bool PlayerPlayFirst { get; set; } = true;

    [HideInInspector]
    public static CombatManager Instance { get; private set; }

    private CardManager _cardManager;
    private HandController _handController;
    private Deck _deck;
    private Discard _discard;
    private GameObject _player;

    private bool _isPlayerTurn;
    public bool IsPlayerTurn {
        get => _isPlayerTurn;
        private set { 
            _isPlayerTurn = value; 
            Debug.Log(_isPlayerTurn ? "Player turn" : "Enemy turn"); 
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start() //TODO: Remove (for testing purposes only)
    {
        _cardManager = CardManager.Instance;
        _handController = GetComponent<HandController>();
        _deck = GetComponent<Deck>();
        _discard = GetComponent<Discard>();
        _player = GameObject.FindWithTag("Player");
        StartFight();
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            BattleOver(1);
        }
        else if (_player.GetComponent<Entity>().getHP() <= 0)
        {
            BattleOver(0);
        }
    }


    public void StartFight()
    {
        Debug.Log("Fight started");
        IsPlayerTurn = PlayerPlayFirst;
        
        //TODO: Remove (for testing purposes only)
        // CardStats[] cards =
        // {
        //     new CardStats("Attack", CardType.Attack, CardRarity.Common, 1, new List<Effect> {new DealDamage(1, 5)}),
        //     
        // };
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject gameObject in enemyObjects) 
        {
            enemies.Add(gameObject.GetComponent<Entity>());
        }
        
        PlayerTurn();
    }
    
    private void PlayerTurn()
    {
        _handController.NewHand();
        _cardManager.enabled = true;
    }

    public void EndTurn()
    {
        _cardManager.enabled = false;
        foreach (Entity enemy in enemies)
        {
            Debug.Log("Enemy [" + enemy + "] health: " + enemy.currentHP);
        }
        
        CheckCards();
        
        _cardManager.ResetMana();
        
        IsPlayerTurn = !IsPlayerTurn;
        if (!IsPlayerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
        else
        {
            PlayerTurn();
        }
    }

    private void CheckCards()
    {
        ClearHand();
        if (_deck.deckSize == 0)
        {
            RefreshDeck();
        }
    }

    private void RefreshDeck()
    {
        _deck.RefreshDeck(Discard.DiscardPile);
        Discard.DiscardPile.Clear();
    }

    private void ClearHand()
    {
        List<ActualCard> hand = _handController.GetHand();
        Discard.DiscardHand(hand);
        _handController.ClearHand();
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f); //TODO: Replace with enemy turn logic
        EndTurn();
    }

    // made it public so I could test it. hopefully didn't mess it up too bad --Matthew
    public void BattleOver(int result)
    {
        if (result == 1)
        {
            Debug.Log("Battle won!");
        }
        else
        {
            Debug.Log("Battle lost...");
        }
        
        resultsWindow.SetActive(true);
    }

}
