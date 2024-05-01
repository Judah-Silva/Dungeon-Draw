using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    private List<Entity> _enemyEntities = new List<Entity>();
    private List<Enemy> _enemyScripts = new List<Enemy>();
    
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

    private SceneRouter _sceneRouter;
    private CardManager _cardManager;
    private HandController _handController;
    private Deck _deck;
    private Discard _discard;
    private GameObject _player;
    private Entity _playerEntity;
    private bool battleOver = false;

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
        _sceneRouter = GameManager.Instance.GetSceneRouter();
        _cardManager = CardManager.Instance;
        _handController = GetComponent<HandController>();
        _deck = GetComponent<Deck>();
        _discard = GetComponent<Discard>();
        _player = GameObject.FindWithTag("Player");
        _playerEntity = _player.GetComponent<Entity>();
        StartFight();
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            BattleOver(1);
        }
        else if (_playerEntity.getHP() <= 0)
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
        foreach (GameObject obj in enemyObjects) 
        {
            enemies.Add(obj);
            _enemyEntities.Add(obj.GetComponent<Entity>());
            _enemyScripts.Add(obj.GetComponent<Enemy>());
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
        if (battleOver)
        {
            Debug.Log("Battle is over.");
            return;
        } 
        _cardManager.enabled = false;
        DisplayState();
        
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

    private void DisplayState()
    {
        Debug.Log($"Player health: {_playerEntity.getHP()}\nPlayer shields: {_playerEntity.getShield()}");
        foreach (var enemy in _enemyEntities)
        {
            Debug.Log($"{enemy.name} health: {enemy.getHP()}\n{enemy.name} shield: {enemy.getShield()}");
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

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        _enemyEntities.Remove(enemy.GetComponent<Entity>());
        _enemyScripts.Remove(enemy.GetComponent<Enemy>());
    }

    private IEnumerator EnemyTurn()
    {
        foreach (var enemy in _enemyScripts)
        {
            enemy.Attack();
            yield return new WaitForSeconds(.5f); //TODO: Replace with enemy turn logic
        }
        EndTurn();
    }

    private void BattleOver(int result)
    {
        battleOver = true;
        if (result == 1)
        {
            Debug.Log("Battle won!");
            // Do whatever needs to be done
            ClearHand();
            // Show rewards, but temporarily just go back to map
            _sceneRouter.ToMap();
            enabled = false;
        }
        else
        {
            Debug.Log("Battle lost...");
        }
    }

    public Entity GetPlayerEntity()
    {
        return _playerEntity;
    }

    public List<Entity> GetEnemyEntities()
    {
        return _enemyEntities;
    }

}
