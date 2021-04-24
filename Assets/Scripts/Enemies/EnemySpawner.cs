using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable
{
    private IEnemyFactory _enemyFactory;
    private List<EnemyInstaller.EnemySpawnSettings> _spawnSettings;

    public EnemySpawner(IEnemyFactory enemyFactory,
        List<EnemyInstaller.EnemySpawnSettings> spawnSettings)
    {
        _enemyFactory = enemyFactory;
        _spawnSettings = spawnSettings;
    }

    public void Initialize()
    {
        foreach (var spawn in _spawnSettings)
        {
            _enemyFactory.Create(spawn.enemyPrefab, spawn.startPosition.position);
        }
    }
}
