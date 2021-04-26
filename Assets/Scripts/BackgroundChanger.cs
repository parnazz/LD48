using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _backrounds;

    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private float _timeToFadeIn;

    [SerializeField]
    private float _timeToFadeOut;

    private int _currentBGIndex;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<ChangeBackgroundSignal>(ChangeBackground);
    }

    void Start()
    {
        _currentBGIndex = 0;
    }

    private void ChangeBackground(ChangeBackgroundSignal signal)
    {
        StartCoroutine(ChangeBackgroundCoroutine());
    }

    IEnumerator ChangeBackgroundCoroutine()
    {
        for (float i = 0; i <= _timeToFadeIn; i += Time.deltaTime)
        {
            _fadeImage.color = new Color(_fadeImage.color.r,
                _fadeImage.color.g,
                _fadeImage.color.b,
                i);
            yield return null; 
        }

        _backrounds[_currentBGIndex].SetActive(false);
        _currentBGIndex++;
        _backrounds[_currentBGIndex].SetActive(true);

        for (float i = _timeToFadeOut; i >= 0; i -= Time.deltaTime)
        {
            _fadeImage.color = new Color(_fadeImage.color.r,
                _fadeImage.color.g,
                _fadeImage.color.b,
                i);
            yield return null;
        }
    }
}
