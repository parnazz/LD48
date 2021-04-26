using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected CharacterStats _currentStats;

    [SerializeField]
    protected Sprite _idleSprite;

    [SerializeField]
    protected Sprite _attackSprite;

    [SerializeField]
    protected AudioClip _attackSound;

    protected Rigidbody2D _rb;
    protected SignalBus _signalBus;
    protected SpriteRenderer _spriteRenderer;

    protected float _attackDelay = 2f;

    public CharacterStats CurrentStats => _currentStats;

    [Inject]
    protected void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public virtual void DoDamage(DamageSignal signal)
    {
        StartCoroutine(ChangeSpriteCoroutine(_attackSprite));
        _signalBus.Fire(signal);
        _signalBus.Fire(new SFXSignal { clip = _attackSound });
    }

    protected IEnumerator ChangeSpriteCoroutine(Sprite sprite, float duration = 0.5f)
    {
        _spriteRenderer.sprite = sprite;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.sprite = _idleSprite;
    }

    public virtual void TakeDamage(DamageSignal signal)
    {
        if (signal.sender == this) return;

        if (signal.reciever == this)
        {
            _currentStats.currentHealth -= signal.sender.CurrentStats.damage;
            _attackDelay = 0;
        }
    }

    protected IEnumerator DoDamageCoroutine(Character character)
    {
        while (character.CurrentStats.currentHealth > 0)
        {
            while (_attackDelay < 2f)
            {
                _attackDelay += Time.deltaTime;
                yield return null;
            }

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
