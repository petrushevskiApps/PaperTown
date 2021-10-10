using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameStarted = new UnityEvent();
    public UnityEvent OnLevelStarted = new UnityEvent();
    public UnityEvent OnLevelPaused = new UnityEvent();
    public UnityEvent OnLevelResumed = new UnityEvent();
    public UnityEvent OnLevelExited = new UnityEvent();
    public UnityEvent OnLevelCompleted = new UnityEvent();
    public UnityEvent OnLevelFailed = new UnityEvent();

    public UnityEvent OnPointsEarned = new UnityEvent();
    public UnityEvent OnProgressMade = new UnityEvent();

    [SerializeField] private GameObject player;

    public static GameManager Instance;

    private float levelProgress = 0;
    
    private int levelPoints = 0;

    private PointsManager pointsManager;
    public float LevelProgress
    {
        get => levelProgress;
        set
        {
            if (value > 0)
            {
                if (Math.Abs(value - levelProgress) > 0.0001f)
                {
                    levelProgress = value;
                    OnProgressMade.Invoke();
                }
            }
            else
            {
                levelProgress = 0;
            }
        }
    }


    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            OnGameStarted.Invoke();
            DontDestroyOnLoad(gameObject);
        }

        pointsManager = GetComponent<PointsManager>();
        pointsManager.OnColorPoints.AddListener(OnPointsAdded);
    }

    private void OnPointsAdded(int points)
    {
        levelPoints += points;
        LevelProgress = levelPoints / 1000f;
    }

    public void StartLevel()
    {
        LevelProgress = 0;
        OnLevelStarted.Invoke();
        player.SetActive(true);
    }
    public void PauseLevel()
    {
        Time.timeScale = 0;
        OnLevelPaused.Invoke();
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
        OnLevelResumed.Invoke();
    }
    public void ExitLevel()
    {
        LevelProgress = 0;
        OnLevelExited.Invoke();
        player.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LevelFailed()
    {
        OnLevelFailed.Invoke();
        player.SetActive(false);
    }

    public void LevelCompleted()
    {
        OnLevelCompleted.Invoke();
        player.SetActive(false);
    }

}
