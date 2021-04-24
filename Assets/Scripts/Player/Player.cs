using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : Character
{
    [SerializeField]
    private float _playerSpeed = 5f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _signalBus.Subscribe<MoveSignal>(Move);
        _signalBus.Subscribe<DamageSignal>(TakeDamage);
    }

    void Start()
    {
        OnHealthChanged();
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
