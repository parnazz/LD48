using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<PlayerStatsChangedSignal>();
        Container.DeclareSignal<GameOverSignal>();
    }
}