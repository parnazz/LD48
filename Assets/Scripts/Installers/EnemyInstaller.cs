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
        public List<GameObject> enemyPrefabs;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_enemySpawnSettings);

        //Container.BindFactory<Enemy, IEnemyFactory>().
        //Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(_enemyPrefab);
        Container.Bind<IEnemyFactory>().To<CustomEnemyFactory>().AsSingle();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.Bind<EnemyStorage>().AsSingle();
    }
}