using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private Player _playerPrefab;

    [SerializeField]
    private PlayerSpawnSettings _playerSpawnSettings;

    [Serializable]
    public class PlayerSpawnSettings
    {
        public Transform startPosition;
    }

    public override void InstallBindings()
    {
        Container.DeclareSignal<MoveSignal>();
        Container.DeclareSignal<PlayerSpawnedSignal>();
        Container.DeclareSignal<DamageSignal>();

        Container.BindInterfacesTo<PlayerInput>().AsSingle();

        Container.Bind<CameraFollow>().AsSingle();
        Container.BindInterfacesTo<PlayerSpawner>().AsSingle();
        Container.BindInstance(_playerSpawnSettings);
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(_playerPrefab);
    }
}