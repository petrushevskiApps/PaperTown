using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class InGameHud : UIScreen
{
    [SerializeField] private Image progressIndicator;


    private void Awake()
    {
        GameManager.Instance.OnProgressMade.AddListener(OnProgressMade);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnProgressMade.RemoveListener(OnProgressMade);
    }

    private void OnProgressMade()
    {
        progressIndicator.fillAmount = GameManager.Instance.LevelProgress;
    }
}
