using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private List<EnemySpawnSettings> _enemySpawnSettings;

    [Serializable]
    public class EnemySpawnSettings
    {
        public Transform startPosition;
        public GameObject enemyPrefab;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_enemySpawnSettings);

        Container.Bind<IEnemyFactory>().To<CustomEnemyFactory>().AsSingle();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.Bind<EnemyStorage>().AsSingle();
    }
}