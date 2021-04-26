using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class StatsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _currentHP;

    [SerializeField]
    private TMP_Text _currentDamage;

    [SerializeField]
    private TMP_Text _currentDefense;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerStatsChangedSignal>(SetStatsText);
    }

    private void SetStatsText(PlayerStatsChangedSignal signal)
    {
        _currentHP.text = signal.stats.currentHealth.ToString();
        _currentDamage.text = signal.stats.damage.ToString();
        _currentDefense.text = signal.stats.defense.ToString();
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayerStatsChangedSignal>(SetStatsText);
    }
}
