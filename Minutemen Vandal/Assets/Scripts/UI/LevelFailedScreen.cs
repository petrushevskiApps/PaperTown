using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelFailedScreen : UIScreen
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainScreenButton;
    [SerializeField] private TextMeshProUGUI failedText;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgainClicked);
        mainScreenButton.onClick.AddListener(GoToMainScreenClicked);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveListener(PlayAgainClicked);
        mainScreenButton.onClick.RemoveListener(GoToMainScreenClicked);
    }
    private void OnEnable()
    {
        SetText();
    }
    private void SetText()
    {
        failedText.text = $"Level # Failed";
    }
    private void PlayAgainClicked()
    {
        GameManager.Instance.StartLevel();
    }

    private void GoToMainScreenClicked()
    {
        GameManager.Instance.ExitLevel();
    }
}
