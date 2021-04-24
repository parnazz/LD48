using System;
using UnityEngine;
using Zenject;

public class MainGameInstaller : MonoInstaller
{
    [SerializeField]
    private MainGameSettings _settings;

    [Serializable]
    public class MainGameSettings
    {
        public string nextSceneName;
    }

    public override void InstallBindings()
    {
        Container.DeclareSignal<DamageSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<FightBeginSignal>();

        Container.BindInstance(_settings);
        Container.BindInterfacesTo<GameController>().AsSingle();
        Container.Bind<SceneController>().AsSingle();
    }
}