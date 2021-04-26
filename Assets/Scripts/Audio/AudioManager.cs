using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _ambientSounds;

    [SerializeField]
    private List<AudioClip> _environmentSounds;

    [SerializeField]
    private AudioSource _ambientSource;

    [SerializeField]
    private AudioSource _environmentSource;

    [SerializeField]
    private AudioSource _combatSource;

    private SignalBus _signalBus;
    

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<SFXSignal>(OnCombatSound);
    }

    private void OnCombatSound(SFXSignal signal)
    {
        if (signal.clip == null) return;
        
        _combatSource.PlayOneShot(signal.clip);
    }
}
