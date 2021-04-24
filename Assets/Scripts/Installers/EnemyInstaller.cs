using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private EnemySpawnSettings _enemySpawnSettings;

    [Serializable]
    public class EnemySpawnSettings
    {
        public List<Transform> startPositions;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_enemySpawnSettings);
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(_enemyPrefab);
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.Bind<EnemyStorage>().AsSingle();
    }
}