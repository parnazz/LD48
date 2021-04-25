using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class FloatingUI : MonoBehaviour
{
    [SerializeField]
    private FollowWorld _enemyHealth;

    private Enemy _enemyToFollow;
    private TMP_Text _enemyHealthText;
    private float _enemyMaxHealth;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<FightBeginSignal>(OnFightBegin);
        _signalBus.Subscribe<UpdateEnemyHealthSignal>(UpdateEnemyHealthText);

        _enemyHealthText = _enemyHealth.GetComponent<TMP_Text>();
    }

    private void OnFightBegin(FightBeginSignal signal)
    {
        _enemyHealth.gameObject.SetActive(true);
        _enemyHealth.SetLookAt(signal.reciever.transform);

        _enemyToFollow = signal.reciever;
        _enemyMaxHealth = signal.reciever.CurrentStats.currentHealth;
        _enemyHealthText.text = _enemyMaxHealth + " / " + _enemyMaxHealth;
    }

    private void UpdateEnemyHealthText(UpdateEnemyHealthSignal signal)
    {
        if (_enemyToFollow == null) return;

        if (_enemyToFollow == signal.enemy)
        {
            _enemyHealthText.text = _enemyMaxHealth + " / " + signal.enemy.CurrentStats.currentHealth;

            if (signal.enemy.CurrentStats.currentHealth <= 0)
            {
                _enemyHealth.gameObject.SetActive(false);
            }
        }
    }
}
