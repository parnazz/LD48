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
    private float _blockCooldown = 0.5f;

    public PlayerInput(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        _gameState = signal.gameState;
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
            Debug.Log("Blocking");

            _timeWhenBlocked = Time.time + _blockCooldown;
            _signalBus.Fire(new BlockSignal { });
        }
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
}
