using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _exitButton;
    private SceneController _sceneController;
    private MainMenuInstaller.MainMenuSettings _settings;

    [Inject]
    private void Construct(SceneController sceneController,
        MainMenuInstaller.MainMenuSettings settings)
    {
        _sceneController = sceneController;
        _settings = settings;
    }

    void Start()
    {
        _startButton.onClick.AddListener(NextScene);
        _exitButton.onClick.AddListener(Exit);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(0);
    }
}
