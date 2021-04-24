using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable
{
    private IEnemyFactory _enemyFactory;
    private EnemyInstaller.EnemySpawnSettings _spawnSettings;

    public EnemySpawner(IEnemyFactory enemyFactory,
        EnemyInstaller.EnemySpawnSettings spawnSettings)
    {
        _enemyFactory = enemyFactory;
        _spawnSettings = spawnSettings;
    }

    public void Initialize()
    {
        for (int i = 0; i < _spawnSettings.startPositions.Count; i++)
        {
            _enemyFactory.Create(_spawnSettings.enemyPrefabs[i], _spawnSettings.startPositions[i].position);
        }
    }
}
