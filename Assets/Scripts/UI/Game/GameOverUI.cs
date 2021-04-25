using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverGO;

    [SerializeField]
    private Button _restartButton;

    private SignalBus _signalBus;
    private SceneController _sceneController;

    [Inject]
    private void Construct(SignalBus signalBus,
        SceneController sceneController)
    {
        _signalBus = signalBus;
        _sceneController = sceneController;
    }

    private void Awake()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChange);

        _restartButton.onClick.AddListener(_sceneController.RestartScene);
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        if (signal.gameState == GameState.GameOverState)
        {
            _gameOverGO.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChange);
    }
}
