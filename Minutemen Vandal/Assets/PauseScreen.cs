using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    [SerializeField] private Button resumeLevel;
    [SerializeField] private Button exitLevel;

    private void Awake()
    {
        resumeLevel.onClick.AddListener(ResumeLevelClicked);
        exitLevel.onClick.AddListener(ExitLevelClicked);
    }

    private void OnDestroy()
    {
        resumeLevel.onClick.RemoveListener(ResumeLevelClicked);
        exitLevel.onClick.RemoveListener(ExitLevelClicked);
    }

    private void ResumeLevelClicked()
    {
        GameManager.Instance.ResumeLevel();
    }

    private void ExitLevelClicked()
    {
        GameManager.Instance.ExitLevel();
    }
}
