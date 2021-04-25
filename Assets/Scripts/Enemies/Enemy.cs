using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : Character
{
    [SerializeField]
    private int _enemyLevel;

    [SerializeField]
    private LootTable _lootTable;

    private EnemyStorage _storage;
    private Player _player;
    
    [SerializeField]
    private Item _lootDrop;

    [Inject]
    private void Constuct(EnemyStorage storage,
        SignalBus signalBus)
    {
        _storage = storage;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<DamageSignal>(TakeDamage);
        _signalBus.Subscribe<FightBeginSignal>(OnFightBegin);
        SetRandomLootDrop();
    }

    private void SetRandomLootDrop()
    {
        var index = Random.Range(0, 3);
        _lootDrop = _lootTable.lootTable[_enemyLevel].equipment[index];
    }

    void Start()
    {
        _storage.enemies.Add(this);
    }

    private void OnFightBegin(FightBeginSignal signal)
    {
        if (signal.reciever != this) return;

        _player = signal.sender;
        if (_player == null) return;

        StartCoroutine(DoDamageCoroutine(_player));
    }

    override public void TakeDamage(DamageSignal signal)
    {
        base.TakeDamage(signal);
        OnEnemyDeath();
    }

    private void OnEnemyDeath()
    {
        if (_currentStats.currentHealth <= 0)
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
            _signalBus.Fire(new LootDropSignal { item = _lootDrop });
            gameObject.SetActive(false);
            Destroy(this.gameObject, 3);
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<DamageSignal>(TakeDamage);
        _signalBus.Unsubscribe<FightBeginSignal>(OnFightBegin);

        _storage.enemies.Remove(this);
    }
}