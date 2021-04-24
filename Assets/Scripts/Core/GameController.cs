using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : IInitializable
{
    private GameState _gameState;

    private SignalBus _signalBus;

    public GameController(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _gameState = GameState.ExploreState;
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        _signalBus.Fire(new GameStateChangedSignal { gameState = _gameState });
    }
}
