using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _pointsText;

    public void SetPointsText(string text)
    {
        _pointsText.text = text;
        Destroy(gameObject, 1);
    }
}
