using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private CombatStats _playerStats;

    private Rigidbody2D _rb;

    private SignalBus _signalBus;

    public CombatStats PlayerStats => _playerStats;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _signalBus.Subscribe<MoveSignal>(Move);

        _signalBus.Fire(new PlayerStatsChangedSignal { stats = _playerStats });
    }

    private void Move(MoveSignal signal)
    {
        _rb.position = (Vector2)transform.position + signal.moveInput * Time.fixedDeltaTime * _playerSpeed;
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
