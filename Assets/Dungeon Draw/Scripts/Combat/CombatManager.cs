using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatManager : MonoBehaviour
{

    public GameObject resultsWindow;
    public TextMeshProUGUI rewardText;
    public int earnedGold;
    
    public bool PlayerPlayFirst { get; set; } = true;
    public List<Level> levels = new ();
    public GameObject player;

    public float tapeGain = 0.5f;
    
    private int _currentLevel = 0;
    public bool isBoss = false; // made public to start boss level easier -- matthew
    
    private List<GameObject> enemies = new List<GameObject>();
    private List<Enemy> _enemyScripts = new List<Enemy>();

    public int originOffset = 7;
    
    /*
    Use StartFight() to start the fight
    It waits for the player to play first if PlayerPlayFirst is true
    Use the end turn button to end the player turn (EndTurn())
    Play the enemy turn with the coroutine EnemyTurn() (TODO: Replace with enemy turn logic)
    And wait for the player to play again
     */


    [HideInInspector]
    public static CombatManager Instance { get; private set; }

    private SceneRouter _sceneRouter;
    private CardManager _cardManager;
    private HandController _handController;
    private Deck _deck;
    private Discard _discard;
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

    public bool IsBoss()
    {
        return isBoss;
    }

    private void Start() //TODO: Remove (for testing purposes only)
    {
        earnedGold = 0;
        
        _sceneRouter = GameManager.Instance.GetSceneRouter();
        _cardManager = CardManager.Instance;
        _handController = GetComponent<HandController>();
        _sceneRouter = GameManager.Instance.GetSceneRouter();
        _deck = GetComponent<Deck>();
        _discard = GetComponent<Discard>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        _playerEntity = player.GetComponent<Player>();
        
        StartFight();
    }

    private void Update()
    {
        if (battleOver)
        {
            return;
        }
        if (enemies.Count == 0)
        {
            //Check for relic id:9 - Heals 5 HP after winning a combat encounter
            if (_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(9)) {
                // _playerEntity.gameObject.GetComponent<Player>().playerStats.UpdateHealth(PlayerStats.CurrentHealth+5);
                _playerEntity.currentHP += 5;
                Debug.Log("Relic: Ball and String Toy: Grants the user 5 HP");
            }
            BattleOver(1);
        }
        else if (_playerEntity.getHP() <= 0 && !_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(10)) //Checks for revive relic id:10
        {
            BattleOver(0);
        }
        else if (_playerEntity.getHP() <= 0 &&
                 _playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(10))
        {
            _playerEntity.currentHP = PlayerStats.MaxHealth / 3;
            _playerEntity.gameObject.GetComponent<Player>().playerStats.removeRelic(RelicDatabase.getRelic(10));
            Debug.Log("Player revived with " + PlayerStats.MaxHealth/3);
        } 
            

        
        //For testing purposes only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enemy enemy = enemies[0].GetComponent<Enemy>();
            enemy.TakeDamage(1);
        }
    }

    public void SpawnWave()
    {
        if (!isBoss)
        {
            foreach (GameObject prefab in levels[_currentLevel].enemies)
            {
                GameObject goEnemy = Instantiate(prefab, new Vector3(0, 0, 0), prefab.transform.rotation); // bc prefab now has initial rotation
                enemies.Add(goEnemy);
                Enemy enemy = goEnemy.GetComponent<Enemy>();
                enemy.SetUp();
                _enemyScripts.Add(enemy);
                /*
                if (enemy.isBoss)
                {
                    isBoss = true;
                }
                */ // removed to make boss fight easier to implement -- matthew
                Debug.Log($"Enemy {enemy.name} added");
            }
            UpdateEnemiesPosition();
            DisplayState();
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
        /*
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject obj in enemyObjects) 
        {
            enemies.Add(obj);
            _enemyEntities.Add(obj.GetComponent<Entity>());
            _enemyScripts.Add(obj.GetComponent<Enemy>());
        }*/
        
        _playerEntity.SetUp();
        SpawnWave();
        //Checks for Big Toe relic and raises maxMana by 1
        if (_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(1))
        {
            _cardManager.maxMana++;
            _cardManager.currentMana = _cardManager.maxMana;
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
            //Check for end turn relics:
            //Cookie id:6 - If you end your turn without block, gain 3 block
            if (_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(6) &&
                _playerEntity.getShield() == 0) {
                _playerEntity.giveShield(3);
                Debug.Log("Relic: Cookie : Grants the user 3 shield");
            }

            //Shell id:12 - Grants the user 1 shield after every combat round
            if (_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(12)) {
                _playerEntity.giveShield(1);
                Debug.Log("Relic: Shell : Grants the user +1 shield");
            }
            
            
            StartCoroutine(EnemyTurn());
        }
        else
        {
            PlayerTurn();
            GameManager.Instance.GetPlayerStats().GainTape(tapeGain);
        }
    }

    private void DisplayState()
    {
        Debug.Log($"Player health: {GetPlayerEntity().getHP()}\nPlayer shields: {GetPlayerEntity().getShield()}");
        foreach (var enemy in _enemyScripts)
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
        List<ActualCard> hand = GetHandController().GetHand();
        Discard.DiscardHand(hand);
        _handController.ClearHand();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        _enemyScripts.Remove(enemy.GetComponent<Enemy>());
        Destroy(enemy);
        UpdateEnemiesPosition();
        
        //Check for green liquid relic(id:5) - gives player 1 mana on enemy death and adds 1 card to hand
        if (_playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(5))
        {
            string str = "Relic: Green Liquid : Grants the user 1 extra mana";
            _cardManager.currentMana += 1;
            if (Deck.DeckPile.Count > 0)
            {
                _handController.AddCardToHand();
                str += (" and draws 1 card");
            }

            Debug.Log(str);
        }
    }

    private IEnumerator EnemyTurn()
    {
        foreach (var enemy in _enemyScripts)
        {
            enemy.Attack();
            //Check if player has purple turtle relic then removes damage done if damage is taken for the first time
            if (_playerEntity.firstDamageTaken > 0 &&
                _playerEntity.gameObject.GetComponent<Player>().playerStats.checkForRelic(4))
            {
                _playerEntity.currentHP += _playerEntity.firstDamageTaken;
                _playerEntity.firstDamageTaken = -2;
                Debug.Log("Purple Turtle blocked incoming damage");
            }
            yield return new WaitForSeconds(.5f); //TODO: Replace with enemy turn logic
        }
        EndTurn();
    }

    // made it public so I could test it. hopefully didn't mess it up too bad --Matthew
    public void BattleOver(int result)
    {
        DisplayState();
        battleOver = true;
        if (result == 1)
        {
            Debug.Log("Battle won!");
            // Do whatever needs to be done
            ClearHand();
            // Show rewards, but temporarily just go back to map
            Invoke("ActivateResultsWindow", 1.0f);
            // _sceneRouter.ToMap();
            enabled = false;
            
            NextLevel();
        }
        else
        {
            Debug.Log("Battle lost...");
        }
        
    }

    public void ActivateResultsWindow()
    {
        resultsWindow.SetActive(true);
        rewardText.text = "+" + earnedGold + " gold";
    }

    public void ToGameOver()
    {
        _sceneRouter.ToGameOver();
    }

    public Entity GetPlayerEntity()
    {
        if (_playerEntity == null)
        {
            if (player is null)
                player = GameObject.FindGameObjectWithTag("Player");
            _playerEntity = player.GetComponent<Player>();
        }
        return _playerEntity;
    }
    
    public HandController GetHandController()
    {
        if (_handController is null)
            _handController = GetComponent<HandController>();
        return _handController;
    }

    public List<Entity> GetEnemyEntities()
    {
        return _enemyScripts.ConvertAll(e => (Entity)e);
    }
    
    public void UpdateEnemiesPosition()
    {
        //Each enemy will be placed in a different position
        //Example :
        //      x    
        //    x   x  
        //  x   x   x
        int distanceBetweenEnemies = 6;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = new Vector3(i * distanceBetweenEnemies - (float)(distanceBetweenEnemies * (enemies.Count - 1) / 2.0 - originOffset), 2, 0);
        }
    }
    
    public void NextLevel()
    {
        if (_currentLevel < levels.Count - 1)
        {
            _currentLevel++;
            SpawnWave();
            battleOver = false;
            StartFight();
        }
        else
        {
            Debug.Log("All levels cleared!");
        }
    }
    
    public void SetLevels(int level)
    {
        _currentLevel = level;
    }
    
    public void SetLevelAndFight(int level)
    {
        _currentLevel = level;
        battleOver = false;
        StartFight();
    }
}
