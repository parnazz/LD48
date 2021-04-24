using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _currentHP;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerStatsChangedSignal>(SetHPText);
    }

    private void SetHPText(PlayerStatsChangedSignal signal)
    {
        _currentHP.text = signal.stats._currentHealth.ToString();
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayerStatsChangedSignal>(SetHPText);
    }
}
