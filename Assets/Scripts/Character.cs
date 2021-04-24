using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected BaseStats _characterBaseStats;

    [SerializeField]
    protected CharacterStats _currentStats;

    public Rigidbody2D _rb;

    protected SignalBus _signalBus;

    public BaseStats BaseStats => _characterBaseStats;
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
            _currentStats._currentHealth -= signal.sender.CurrentStats._damage;
        }
    }

    [Serializable]
    public class CharacterStats
    {
        public float _currentHealth;
        public float _defense;
        public float _damage;
        public float _attackSpeed;
    }
}
