using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class InGameHud : UIScreen
{
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image progressIndicator;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        GameManager.Instance.OnLevelStarted.AddListener(OnLevelStarted);
        GameManager.Instance.OnProgressMade.AddListener(OnProgressMade);
    }

    

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted.RemoveListener(OnLevelStarted);
        GameManager.Instance.OnProgressMade.RemoveListener(OnProgressMade);
    }
    private void OnLevelStarted()
    {
        SetProgress(0);
    }

    private void OnEnable()
    {
        if (!GameManager.Instance.IsGameCompleted)
        {
            levelText.text = $"Level {GameManager.Instance.LevelId + 1}";
            progressBar.gameObject.SetActive(true);
        }
        else
        {
            levelText.text = "Infinity Coloring";
            progressBar.gameObject.SetActive(false);
        }
    }
    private void OnProgressMade()
    {
        SetProgress(GameManager.Instance.LevelProgress);
    }

    private void SetProgress(float percentage)
    {
        progressIndicator.fillAmount = percentage;
        progressText.text =
            $"{GameManager.Instance.LevelPoints} / {GameManager.Instance.GetCurrentLevelData().pointsToEarnInLevel}";
    }
}
