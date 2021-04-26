using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _ambientSounds;

    [SerializeField]
    private AudioClip[] _environmentSounds;

    [SerializeField]
    private AudioSource _ambientSource;

    [SerializeField]
    private AudioSource _environmentSource;

    [SerializeField]
    private AudioSource _combatSource;

    [SerializeField]
    private Vector2 _minMaxTimeBetweenSounds;

    private SignalBus _signalBus;

    private int _currentAmbientIndex;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<SFXSignal>(OnCombatSound);
        _signalBus.Subscribe<ChangeBackgroundSignal>(OnChangeAmbient);

        _currentAmbientIndex = 0;
    }

    private void Start()
    {
        StartCoroutine(PlayRandomEnvironmentSound());
    }

    private void OnChangeAmbient(ChangeBackgroundSignal signal)
    {
        _currentAmbientIndex++;
        if (_currentAmbientIndex < _ambientSounds.Count)
        {
            _ambientSource.clip = _ambientSounds[_currentAmbientIndex];
            _ambientSource.Play();
        }
    }

    private void OnCombatSound(SFXSignal signal)
    {
        if (signal.clip == null) return;
        
        _combatSource.PlayOneShot(signal.clip);
    }

    private IEnumerator PlayRandomEnvironmentSound()
    {
        while (true)
        {
            int index = Random.Range(0, _environmentSounds.Length);
            float time = Random.Range(_minMaxTimeBetweenSounds.x, _minMaxTimeBetweenSounds.y);
            yield return new WaitForSeconds(time);
            _environmentSource.PlayOneShot(_environmentSounds[index]);
        }
    }
}
