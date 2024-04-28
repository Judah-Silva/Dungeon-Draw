using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    Use StartFight() to start the fight
    It waits for the player to play first if PlayerPlayFirst is true
    Use the end turn button to end the player turn (EndTurn())
    Play the enemy turn with the coroutine EnemyTurn() (TODO: Replace with enemy turn logic)
    And wait for the player to play again
     */
    
    public bool PlayerPlayFirst { get; set; } = true;
    
    [HideInInspector]
    public static GameManager Instance { get; private set; }
    
    private bool _isPlayerTurn;
    public bool IsPlayerTurn {
        get => _isPlayerTurn;
        private set { 
            _isPlayerTurn = value; 
            Debug.Log(_isPlayerTurn ? "Player turn" : "Enemy turn"); 
        }
    }

    private HandController _handController;
    private Deck _deck;
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
        _handController = GetComponent<HandController>();
        _deck = GetComponent<Deck>();
        StartFight();
    }

    public void StartFight()
    {
        Debug.Log("Fight started");
        IsPlayerTurn = PlayerPlayFirst;
        PlayerTurn();
    }
    
    public void EndTurn()
    {
        if (_deck.deckSize == 0)
        {
            _deck.ResetDeck();
        }
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

    private void PlayerTurn()
    {
        // Debug.Log("Hand draw");
        StartCoroutine(_handController.DrawHand());
        // Debug.Log("Hand done drawing");
        // StopCoroutine(_handController.DrawHand());
        // Card targeting and functionality takes over
    }
    
    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2.5f); //TODO: Replace with enemy turn logic
        EndTurn();
    }
}