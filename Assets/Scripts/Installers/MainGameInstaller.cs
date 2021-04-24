using UnityEngine;
using Zenject;

public class MainGameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<DamageSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();

        Container.BindInterfacesTo<GameController>().AsSingle();
    }
}