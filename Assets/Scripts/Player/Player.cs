using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : Character
{
    [SerializeField]
    private Sprite _blockSprite;

    [SerializeField]
    private BaseStats _characterBaseStats;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _criticalHealth = 15f;

    [SerializeField]
    private float _timeToBlock = 1.5f;

    [SerializeField]
    private List<Sprite> _sprites;

    private Enemy _enemyToAttack;
    private float _maxHealth;
    private float _timeWhenBlocked;
    private bool _canBlock;

    public BaseStats BaseStats => _characterBaseStats;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _signalBus.Subscribe<MoveSignal>(Move);
        _signalBus.Subscribe<DamageSignal>(TakeDamage);
        _signalBus.Subscribe<ItemEquipedSignal>(OnItemEquiped);
        _signalBus.Subscribe<UseHealthPotionSignal>(OnHealing);
        _signalBus.Subscribe<BlockSignal>(Block);
        _signalBus.Subscribe<PlayerAttackSignal>(Attack);
        _signalBus.Subscribe<ChangePlayerLookSignal>(ChangeLook);
    }

    void Start()
    {
        SetInitialStats();
        OnStatsChanged();
    }

    private void SetInitialStats()
    {
        _maxHealth = _characterBaseStats.maxHealthPoints;
        _currentStats.currentHealth = _characterBaseStats.maxHealthPoints;
        _currentStats.damage = _characterBaseStats.damage;
        _currentStats.defense = _characterBaseStats.defense;
        _currentStats.attackSpeed = _characterBaseStats.attackSpeed;
    }

    private void ChangeLook(ChangePlayerLookSignal signal)
    {
        _spriteRenderer.sprite = _sprites[signal.index - 1];
    }

    private void OnStatsChanged()
    {
        _signalBus.Fire(new PlayerStatsChangedSignal { stats = CurrentStats });
    }

    private void Move(MoveSignal signal)
    {
        _rb.position = (Vector2)transform.position + signal.moveInput * Time.fixedDeltaTime * _playerSpeed;
    }

    private void Block(BlockSignal signal)
    {
        StartCoroutine(ChangeSpriteCoroutine(_blockSprite, _timeToBlock));
        _canBlock = true;
        _timeWhenBlocked = Time.time;
    }

    private IEnumerator BlockCoroutine()
    {
        _canBlock = true;
        yield return new WaitForSeconds(_timeToBlock);
        _canBlock = false;
    }

    override public void TakeDamage(DamageSignal signal)
    {
        if (signal.sender == this) return;

        if (signal.reciever == this)
        {
            _signalBus.Fire(new AttackBlockedSignal { sender = this, isBlocked = _canBlock });

            var damageTaken = _canBlock ?
                signal.sender.CurrentStats.damage - _currentStats.defense :
                signal.sender.CurrentStats.damage;

            damageTaken = Mathf.Max(0, damageTaken);

            _currentStats.currentHealth -= damageTaken;

            _canBlock = false;
        }

        OnStatsChanged();
        OnCriticalHealth();
        OnDeath();
    }

    private void OnCriticalHealth()
    {
        if (_currentStats.currentHealth <= _criticalHealth)
        {
            _signalBus.Fire(new HealingSignal { });
        }
    }

    private void OnDeath()
    {
        if (_currentStats.currentHealth <= 0)
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.GameOverState });
        }
    }

    private void OnHealing(UseHealthPotionSignal signal)
    {
        _currentStats.currentHealth += signal.item.amountOfHealing;
        _currentStats.currentHealth = Mathf.Clamp(_currentStats.currentHealth,
            _currentStats.currentHealth,
            _maxHealth);

        OnStatsChanged();
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

        OnStatsChanged();
    }

    private void Attack(PlayerAttackSignal signal)
    {
        DamageSignal damageSignal = new DamageSignal { reciever = _enemyToAttack, sender = this };
        DoDamage(damageSignal);
    }

    private void Update()
    {
        if (!_canBlock) return;

        if (Time.time >= _timeWhenBlocked + _timeToBlock)
        {
            _canBlock = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.FightState } );

            _enemyToAttack = collision.GetComponent<Enemy>();
            _signalBus.Fire(new FightBeginSignal { sender = this, reciever = _enemyToAttack });
        }

        if (collision.CompareTag("NPC"))
        {
            _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.DialogState });
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MoveSignal>(Move);
        _signalBus.Unsubscribe<DamageSignal>(TakeDamage);
        _signalBus.Unsubscribe<ItemEquipedSignal>(OnItemEquiped);
        _signalBus.Unsubscribe<UseHealthPotionSignal>(OnHealing);
        _signalBus.Unsubscribe<BlockSignal>(Block);
        _signalBus.Unsubscribe<PlayerAttackSignal>(Attack);
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
