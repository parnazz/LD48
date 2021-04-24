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
        Time.timeScale = 1;
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChange);

        _gameState = GameState.ExploreState;
        OnGameStarted();
    }

    private void OnGameStarted()
    {
        _signalBus.Fire(new GameStateChangedSignal { gameState = _gameState });
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        _gameState = signal.gameState;

        if (_gameState == GameState.GameOverState)
            Time.timeScale = 0;
    }
}
