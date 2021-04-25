using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : IFixedTickable, IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private GameState _gameState;

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

    public void FixedTick()
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

    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
}
