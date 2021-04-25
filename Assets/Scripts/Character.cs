using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected CharacterStats _currentStats;

    protected Rigidbody2D _rb;

    protected SignalBus _signalBus;

    public CharacterStats CurrentStats => _currentStats;

    [Inject]
    protected void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public virtual void DoDamage(DamageSignal signal)
    {
        _signalBus.Fire(signal);
    }

    public virtual void TakeDamage(DamageSignal signal)
    {
        if (signal.sender == this) return;

        if (signal.reciever == this)
        {
            _currentStats.currentHealth -= signal.sender.CurrentStats.damage;
        }
    }

    protected IEnumerator DoDamageCoroutine(Character character)
    {
        while (character.CurrentStats.currentHealth > 0)
        {
            DamageSignal signal = new DamageSignal { reciever = character, sender = this };
            DoDamage(signal);
            yield return new WaitForSeconds(_currentStats.attackSpeed);
        }
    }

    [Serializable]
    public class CharacterStats
    {
        public float currentHealth;
        public float defense;
        public float damage;
        public float attackSpeed;
    }
}
