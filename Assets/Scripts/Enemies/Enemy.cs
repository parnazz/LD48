using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : Character
{
    private EnemyStorage _storage;
    private Player _player;

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

        if (_currentStats._currentHealth <= 0)
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
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

    public class Factory : PlaceholderFactory<Enemy>
    {
        public Enemy Create(Vector3 at)
        {
            var enemy = base.Create();
            enemy.transform.position = at;
            return enemy;
        }
    }
}
