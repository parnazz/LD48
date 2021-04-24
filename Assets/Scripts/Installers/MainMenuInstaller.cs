using System;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuSettings _settings;

    [Serializable]
    public class MainMenuSettings
    {
        public string nextSceneName;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_settings);
        Container.Bind<SceneController>().AsSingle();
    }
}