using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    [SerializeField] private Button resumeLevel;
    [SerializeField] private Button exitLevel;
    [SerializeField] private Button nextWeapon;
    [SerializeField] private Button previousWeapon;
    [SerializeField] private TMP_Text gunNameText;

    private void Awake()
    {
        resumeLevel.onClick.AddListener(ResumeLevelClicked);
        exitLevel.onClick.AddListener(ExitLevelClicked);
        nextWeapon.onClick.AddListener(NextWeaponClicked);
        previousWeapon.onClick.AddListener(PreviousWeaponClicked);
    }

    private void OnDestroy()
    {
        resumeLevel.onClick.RemoveListener(ResumeLevelClicked);
        exitLevel.onClick.RemoveListener(ExitLevelClicked);
    }

    private void ResumeLevelClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.ResumeLevel();
    }

    private void ExitLevelClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        GameManager.Instance.ExitLevel();
    }

    private void NextWeaponClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        gunNameText.text = GameManager.Instance.ChangeGun(true);
    }

    private void PreviousWeaponClicked()
    {
        AudioManager.Instance.OnButtonClicked();
        gunNameText.text = GameManager.Instance.ChangeGun(false);
    }
}
