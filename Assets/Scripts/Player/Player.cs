using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : Character
{
    [SerializeField]
    private float _playerSpeed = 5f;

    private float _maxHealth;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _signalBus.Subscribe<MoveSignal>(Move);
        _signalBus.Subscribe<DamageSignal>(TakeDamage);
        _signalBus.Subscribe<ItemEquipedSignal>(OnItemEquiped);
    }

    void Start()
    {
        SetInitialStats();
        OnHealthChanged();
    }

    private void SetInitialStats()
    {
        _maxHealth = _characterBaseStats.maxHealthPoints;
        _currentStats.currentHealth = _characterBaseStats.maxHealthPoints;
        _currentStats.damage = _characterBaseStats.damage;
        _currentStats.defense = _characterBaseStats.defense;
        _currentStats.attackSpeed = _characterBaseStats.attackSpeed;
    }

    private void OnHealthChanged()
    {
        _signalBus.Fire(new PlayerStatsChangedSignal { stats = CurrentStats });
    }

    private void Move(MoveSignal signal)
    {
        _rb.position = (Vector2)transform.position + signal.moveInput * Time.fixedDeltaTime * _playerSpeed;
    }

    override public void TakeDamage(DamageSignal signal)
    {
        base.TakeDamage(signal);
        OnHealthChanged();

        if (_currentStats.currentHealth <= 0)
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.GameOverState });
        }
    }

    private void OnItemEquiped(ItemEquipedSignal signal)
    {
        if (signal.item.damage != 0)
            _currentStats.damage = signal.item.damage;

        if (signal.item.defense != 0)
            _currentStats.defense = signal.item.defense;

        if (signal.item.health != 0)
        {
            var healthDiff = _maxHealth - _currentStats.currentHealth;
            _maxHealth = signal.item.health;
            _currentStats.currentHealth = _maxHealth - healthDiff;
        }

        OnHealthChanged();

        Debug.Log("Max Health: " + _maxHealth);
        Debug.Log("Current Health: " + _currentStats.currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.FightState } );
           
            var enemy = collision.GetComponent<Enemy>();
            _signalBus.Fire(new FightBeginSignal { sender = this, reciever = enemy });

            StartCoroutine(DoDamageCoroutine(enemy));
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MoveSignal>(Move);
        _signalBus.Unsubscribe<DamageSignal>(TakeDamage);
    }

    public class Factory : PlaceholderFactory<Player>
    {
        public Player Create(Vector2 at)
        {
            var player = base.Create();
            player.transform.position = at;
            return player;
        }
    }
}
