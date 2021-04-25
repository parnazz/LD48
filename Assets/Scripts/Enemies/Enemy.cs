using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : Character
{
    [SerializeField]
    private float _timeToNotify = 0.1f;

    [SerializeField]
    private GameObject _spriteNotifier;

    [SerializeField]
    private int _enemyLevel;

    [SerializeField]
    private LootTable _lootTable;

    private EnemyStorage _storage;
    private Player _player;
    private Item _lootDrop;
    private float _timeToNewAttack;

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
        _timeToNewAttack = _currentStats.attackSpeed;
    }

    private void OnFightBegin(FightBeginSignal signal)
    {
        if (signal.reciever != this) return;

        _player = signal.sender;
        if (_player == null) return;

        _attackDelay = 1f;
        StartCoroutine(DoDamageCoroutine(_player));
    }

    public override void DoDamage(DamageSignal signal)
    {
        StartCoroutine(BeforeAttackCoroutine(signal));
    }

    private IEnumerator BeforeAttackCoroutine(DamageSignal signal)
    {
        _spriteNotifier.SetActive(true);
        yield return new WaitForSeconds(_timeToNotify);
        _spriteNotifier.SetActive(false);
        base.DoDamage(signal);
    }

    public override void TakeDamage(DamageSignal signal)
    {
        base.TakeDamage(signal);

        _signalBus.Fire(new UpdateEnemyHealthSignal { enemy = this });
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

        if (_storage.enemies.Count <= 0)
        {
            _signalBus.Fire(new LootDropSignal { item = _lootTable.healthPotion });
        }
    }
}