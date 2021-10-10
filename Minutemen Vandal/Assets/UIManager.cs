using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainScreen mainScreen;
    [SerializeField] private InGameHud inGameHud;
    [SerializeField] private LevelFailedScreen levelFailedScreen;
    [SerializeField] private LevelCompletedScreen levelCompletedScreen;
    [SerializeField] private PauseScreen pauseScreen;

    private List<UIScreen> screens = new List<UIScreen>();

    public void Awake()
    {
        GameManager.Instance.OnLevelStarted.AddListener(OnLevelStarted);
        GameManager.Instance.OnLevelPaused.AddListener(OnLevelPaused);
        GameManager.Instance.OnLevelFailed.AddListener(OnLevelFailed);
        GameManager.Instance.OnLevelCompleted.AddListener(OnLevelCompleted);
        GameManager.Instance.OnLevelExited.AddListener(OnLevelExited);
        GameManager.Instance.OnLevelResumed.AddListener(OnLevelStarted);
        SetupScreens();
    }


    public void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted.RemoveListener(OnLevelStarted);
        GameManager.Instance.OnLevelPaused.RemoveListener(OnLevelPaused);
        GameManager.Instance.OnLevelFailed.RemoveListener(OnLevelFailed);
        GameManager.Instance.OnLevelCompleted.RemoveListener(OnLevelCompleted);
        GameManager.Instance.OnLevelExited.RemoveListener(OnLevelExited);
        GameManager.Instance.OnLevelResumed.RemoveListener(OnLevelStarted);
    }

    private void SetupScreens()
    {
        screens.Add(mainScreen);
        screens.Add(inGameHud);
        screens.Add(levelFailedScreen);
        screens.Add(levelCompletedScreen);
        screens.Add(pauseScreen);

        ShowScreen(mainScreen);
    }

    private void ShowScreen(UIScreen showScreen)
    {
        foreach (UIScreen screen in screens)
        {
            if (screen != showScreen)
            {
                screen.gameObject.SetActive(false);
            }
            else
            {
                screen.gameObject.SetActive(true);
            }
        }
    }
    
    private void OnLevelCompleted()
    {
        ShowScreen(levelCompletedScreen);
    }

    private void OnLevelFailed()
    {
        ShowScreen(levelFailedScreen);
    }

    private void OnLevelPaused()
    {
        ShowScreen(pauseScreen);
    }

    private void OnLevelStarted()
    {
        ShowScreen(inGameHud);
    }
    private void OnLevelExited()
    {
        ShowScreen(mainScreen);
    }
}
