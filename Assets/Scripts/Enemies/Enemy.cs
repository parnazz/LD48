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
        _signalBus.Subscribe<PlayerSpawnedSignal>(SetPlayer);
        _signalBus.Subscribe<DamageSignal>(TakeDamage);
    }

    void Start()
    {
        _storage.enemies.Add(this);

        DamageSignal signal = new DamageSignal { reciever = _player, sender = this };
    }

    private void SetPlayer(PlayerSpawnedSignal signal)
    {
        _player = signal.player;
    }

    override public void TakeDamage(DamageSignal signal)
    {
        base.TakeDamage(signal);

        if (_currentStats._currentHealth <= 0)
        {
            Debug.Log("Dead" + this.GetInstanceID());
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
            gameObject.SetActive(false);
            Destroy(this.gameObject, 3);
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayerSpawnedSignal>(SetPlayer);
        _signalBus.Unsubscribe<DamageSignal>(TakeDamage);
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
