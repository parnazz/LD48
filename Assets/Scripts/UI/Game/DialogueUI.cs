using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogueGO;

    [SerializeField]
    private GameObject _choicesContainer;

    private List<Button> _choiceButtons;

    private SignalBus _signalBus;
    private DialogueChoiceCounter _choiceCounter;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChange);

        _choiceCounter = FindObjectOfType<DialogueChoiceCounter>();
        _choiceButtons = new List<Button>(_choicesContainer.GetComponentsInChildren<Button>());
        _choiceButtons[0].onClick.AddListener(PickBadChoice);
        _choiceButtons[1].onClick.AddListener(PickNeutralChoice);
        _choiceButtons[2].onClick.AddListener(PickGoodChoice);
    }

    private void PickBadChoice()
    {
        _choiceCounter.IncreaseBadChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
        _signalBus.Fire(new ChangePlayerLookSignal { index = _choiceCounter.choices[0] });
    }

    private void PickNeutralChoice()
    {
        _choiceCounter.IncreaseNeutralChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
    }

    private void PickGoodChoice()
    {
        _choiceCounter.IncreaseGoodChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        if (signal.gameState == GameState.DialogState)
        {
            _dialogueGO.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChange);
    }
}
