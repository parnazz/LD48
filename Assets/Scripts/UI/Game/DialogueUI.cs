using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    public TMP_Text _dialog;

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
        _choiceButtons[2].onClick.AddListener(PickGoodChoice);
        _choiceButtons[1].onClick.AddListener(PickNeutralChoice);
        _choiceButtons[0].onClick.AddListener(PickBadChoice);
    }

    private void PickBadChoice()
    {
        _choiceCounter.IncreaseNegativeChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
        //_signalBus.Fire(new ChangePlayerLookSignal { index = _choiceCounter.choices[0] });
        _signalBus.Fire(new ChangeBackgroundSignal { });
    }

    private void PickNeutralChoice()
    {
        _choiceCounter.IncreaseNeutralChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
        _signalBus.Fire(new ChangeBackgroundSignal { });
    }

    private void PickGoodChoice()
    {
        _choiceCounter.IncreasePositiveChoices();
        _dialogueGO.SetActive(false);
        _signalBus.Fire(new GameStateChangedSignal { gameState = GameState.ExploreState });
        _signalBus.Fire(new ChangeBackgroundSignal { });
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        if (signal.gameState == GameState.DialogState)
        {
            _dialogueGO.SetActive(true);
            if(signal.npcId == 1)
            {
                _dialog.text = @"Middle son:
Hey, pup! Give me a health potion, quickly! Don’t you see, I’m bleeding!
YOU:
So, maybe you should give me the family shield, if you’re wounded.
Middle son:
You kidding? You can’t even use it properly.Give me the potion, I tell you!";

            } else if(signal.npcId == 2)
            {
                _dialog.text = @"Elder brother:
Brother! What are you doing here? It’s dangerous!I am wounded. But it’s nothing. I’ll have some rest and will go further.
YOU:
I’d better go further, but I need our family armor you wear.
Elder brother
What? No, I can’t give it.I can’t surrender. I am the heir of our house and it is me who will follow the queen’s order.";
            }
            else if (signal.npcId == 3)
            {
                _dialog.text = @"Father:
You wasted so long time coming here. It’s not so hard when I’ve already cleared all the way. I need help, son.A health potion.
YOU:
Father, the cursed roots have almost embraced you completely. You can’t go further.Give me our glorified family sword and let me get the flower. I’ll come back for you.
Father:
I can’t give you the relic of my house.Even if you were ready... You’re adopted. Give me the potion and go home.I’ll complete the quest. That’s my will.";
            }
        }
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChange);
    }
}
