using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject environmentPrefab;
    private GameObject environmentInstance;

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

        if(LevelProgress >= 1f)
        {
            LevelCompleted();
        }
    }

    public void StartLevel()
    {
        ResetEnvironment();
        LevelProgress = 0;
        OnLevelStarted.Invoke();
        player.SetActive(true);
        ToggleCursor(false);
    }
    public void PauseLevel()
    {
        Time.timeScale = 0;
        OnLevelPaused.Invoke();
        ToggleCursor(true);
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
        OnLevelResumed.Invoke();
        ToggleCursor(false);
    }
    public void ExitLevel()
    {
        OnLevelExited.Invoke();
        player.SetActive(false);
        ToggleCursor(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LevelFailed()
    {
        OnLevelFailed.Invoke();
        player.SetActive(false);
        ToggleCursor(true);
    }

    public void LevelCompleted()
    {
        
        OnLevelCompleted.Invoke();
        player.SetActive(false);
        ToggleCursor(true);
    }

    private void ResetEnvironment()
    {
        if (environmentInstance != null)
        {
            environmentInstance.SetActive(false);
            Destroy(environmentInstance);
        }
        LevelProgress = 0;
        levelPoints = 0;
        environmentInstance = Instantiate(environmentPrefab);
    }

    private void ToggleCursor(bool isActive)
    {
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}
