using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : UIScreen
{
    [SerializeField] private Button startLevelButton;
    [SerializeField] private Button exitGameButton;

    private void Awake()
    {
        startLevelButton.onClick.AddListener(StartLevelClicked);
        exitGameButton.onClick.AddListener(ExitClicked);
    }

    private void OnDestroy()
    {
        startLevelButton.onClick.AddListener(StartLevelClicked);
        exitGameButton.onClick.AddListener(ExitClicked);
    }

    private void StartLevelClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.StartLevel();
    }

    private void ExitClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.ExitGame();
    }
}
