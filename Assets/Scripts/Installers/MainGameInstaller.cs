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
        Container.DeclareSignal<LootDropSignal>();
        Container.DeclareSignal<UseItemSignal>();
        Container.DeclareSignal<ItemEquipedSignal>();
        Container.DeclareSignal<HealingSignal>();
        Container.DeclareSignal<UseHealthPotionSignal>();

        Container.BindInstance(_settings);
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        Container.Bind<SceneController>().AsSingle();
    }
}