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
    }

    // Start is called before the first frame update
    void Start()
    {
        _storage.enemies.Add(this);

        DamageSignal signal = new DamageSignal { reciever = _player, sender = this };
        DoDamage(signal);
    }

    private void SetPlayer(PlayerSpawnedSignal signal)
    {
        _player = signal.player;
    }

    // Update is called once per frame
    void Update()
    {
        
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
