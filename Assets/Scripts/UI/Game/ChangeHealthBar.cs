using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangeHealthBar : MonoBehaviour
{
    public Image HPBar;
    SignalBus _signalBus;

    [Inject]
    protected void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

     void CheckHP(PlayerStatsChangedSignal signal)
    {
        if(signal.stats.currentHealth <= 100)
        {
            HPBar.fillAmount = signal.stats.currentHealth / 100f;
        }
        else
        {
            HPBar.fillAmount = 1f;
        }
    }
    private void Awake()
    {
        _signalBus.Subscribe<PlayerStatsChangedSignal>(CheckHP);
    }
}
