using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedScreen : UIScreen
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainScreenButton;
    [SerializeField] private TextMeshProUGUI congratulationsText;
    [SerializeField] private Image rewardImage;


    private void Awake()
    {
        nextLevelButton.onClick.AddListener(PlayAgainClicked);
        mainScreenButton.onClick.AddListener(GoToMainScreenClicked);
    }

    private void OnDestroy()
    {
        nextLevelButton.onClick.RemoveListener(PlayAgainClicked);
        mainScreenButton.onClick.RemoveListener(GoToMainScreenClicked);
    }

    private void OnEnable()
    {
        SetText();
    }

    
    private void SetText()
    {
        if (GameManager.Instance.IsGameCompleted)
        {
            congratulationsText.text = $"All Levels Completed";
        }
        else
        {
            congratulationsText.text = $"Level {GameManager.Instance.LevelId} completed";
            rewardImage.color = GameManager.Instance.GetLevelData(GameManager.Instance.LevelId - 1).levelReward;
        }
        
    }

    private void PlayAgainClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.StartLevel();
    }

    private void GoToMainScreenClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.ExitLevel();
    }
}
