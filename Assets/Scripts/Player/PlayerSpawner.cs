using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSpawner : IInitializable
{
    private Player.Factory _playerFactory;
    private PlayerInstaller.PlayerSpawnSettings _spawnSettings;
    private SignalBus _signalBus;

    public PlayerSpawner(Player.Factory playerFactory, 
        PlayerInstaller.PlayerSpawnSettings spawnSettings,
        SignalBus signalBus)
    {
        _playerFactory = playerFactory;
        _spawnSettings = spawnSettings;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        Player player = _playerFactory.Create(_spawnSettings.startPosition.position);
        _signalBus.Fire(new PlayerSpawnedSignal { playerTransform = player.transform });
    }
}
