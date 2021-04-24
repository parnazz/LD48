using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    private SceneController _sceneController;
    private MainMenuInstaller.MainMenuSettings _settings;

    [Inject]
    private void Construct(SceneController sceneController,
        MainMenuInstaller.MainMenuSettings settings)
    {
        _sceneController = sceneController;
        _settings = settings;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(NextScene);
    }

    private void NextScene()
    {
        _sceneController.LoadScene(_settings.nextSceneName);
    }
}
