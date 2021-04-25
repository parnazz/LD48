using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : ITickable, IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private GameState _gameState;

    private float _timeWhenBlocked = 0;
    private float _blockCooldown = 1f;
    private float _blockDuration = 1.5f;

    private float _timeWhenAttacked = 0;
    private float _attackCooldown = 2.5f;

    private float _missedAttackDelay = 2;
    private float _blockedAttackDelay = 1;

    public PlayerInput(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        _signalBus.Subscribe<AttackBlockedSignal>(OnAttackBlocked);
    }

    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        _gameState = signal.gameState;
    }

    private void OnAttackBlocked(AttackBlockedSignal signal)
    {
        
        var attackDelayDiff = _timeWhenAttacked - Time.time;

        if (attackDelayDiff < 0)
            attackDelayDiff = 0;

        if (signal.isBlocked)
        {
            _timeWhenAttacked = Time.time + attackDelayDiff + _blockedAttackDelay;
        }
        else
        {
            _timeWhenAttacked = Time.time + attackDelayDiff + _missedAttackDelay;
        }
    }

    public void Tick()
    {
        switch(_gameState)
        {
            case GameState.ExploreState:
                OnExploreState();
                break;
            case GameState.FightState:
                Fight();
                break;
            case GameState.GameOverState:
                break;
            case GameState.DialogState:
                break;
            default:
                break;
        }
    }

    private void OnExploreState()
    {
        _signalBus.Fire(new MoveSignal { moveInput = new Vector2(1, 0) });
    }

    private void Fight()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= _timeWhenBlocked)
        {
            _timeWhenBlocked = Time.time + _blockCooldown;
            _signalBus.Fire(new BlockSignal { });
        }

        if (Input.GetMouseButtonDown(0) && 
            Time.time >= _timeWhenAttacked &&
            Time.time >= _timeWhenBlocked - _blockCooldown + _blockDuration)
        {
            _timeWhenAttacked = Time.time + _attackCooldown;
            _signalBus.Fire(new PlayerAttackSignal { });
        }

        if (_timeWhenAttacked - Time.time > 0)
        {
            var time = Mathf.Max(_timeWhenAttacked - Time.time, 0);
            _signalBus.Fire(new UpdateCooldownSignal { value = time });
        }
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
}
