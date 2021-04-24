using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable
{
    private Enemy.Factory _enemyFactory;
    private EnemyInstaller.EnemySpawnSettings _spawnSettings;

    public EnemySpawner(Enemy.Factory enemyFactory,
        EnemyInstaller.EnemySpawnSettings spawnSettings)
    {
        _enemyFactory = enemyFactory;
        _spawnSettings = spawnSettings;
    }

    public void Initialize()
    {
        foreach (var marker in _spawnSettings.startPositions)
        {
            _enemyFactory.Create(marker.position);
        }
    }
}
