using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private const string LEVEL_ID_KEY = "level_id_key";

    [HideInInspector] public UnityEvent OnGameStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelPaused = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelResumed = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelExited = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelCompleted = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelFailed = new UnityEvent();

    [HideInInspector] public UnityEvent OnPointsEarned = new UnityEvent();
    [HideInInspector] public UnityEvent OnProgressMade = new UnityEvent();

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject environmentPrefab;
    [SerializeField] private List<LevelData> listLevelDatas;

    public static GameManager Instance;

    private float levelProgress = 0;
    
    private int levelPoints = 0;

    private PointsManager pointsManager;
    [SerializeField]  private GameObject environmentInstance;

    public int LevelPoints => levelPoints;
    public int LevelId
    {
        get => PlayerPrefs.GetInt(LEVEL_ID_KEY, 0);
        private set
        {
            if (value > 0 && value > LevelId)
            {
                PlayerPrefs.SetInt(LEVEL_ID_KEY, value);
            }
        }
    }

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

    public bool IsGameCompleted => LevelId == listLevelDatas.Count;

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
        if (!IsGameCompleted)
        {
            levelPoints += points;
            LevelProgress = levelPoints / (float)listLevelDatas[LevelId].pointsToEarnInLevel;

            if (LevelProgress >= 1f)
            {
                LevelCompleted();
            }
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
        LevelId++;
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

    public List<Color> GetLevelAvailableColors()
    {
        return GetCurrentLevelData().unlockedColors;
    }

    public LevelData GetCurrentLevelData()
    {
        return GetLevelData(LevelId);
    }
    public LevelData GetLevelData(int levelId)
    {
        if (levelId < listLevelDatas.Count)
        {
            return listLevelDatas[levelId];
        }
        else
        {
            return listLevelDatas[listLevelDatas.Count - 1];
        }
    }
}
