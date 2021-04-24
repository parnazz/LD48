using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerSpawnedSignal>(SetPlayerTransform);
    }

    private void SetPlayerTransform(PlayerSpawnedSignal signal)
    {
        _player = signal.player.transform;
    }

    void LateUpdate()
    {
        if (_player == null) return;

        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayerSpawnedSignal>(SetPlayerTransform);
    }
}
