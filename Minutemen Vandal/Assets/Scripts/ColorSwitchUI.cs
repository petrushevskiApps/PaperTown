using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitchUI : MonoBehaviour
{
    [SerializeField]
    private Image BackgroundPanel;
    [SerializeField]
    private Image ColorPanel;

    [SerializeField]
    private Color _activeColor = Color.white;
    [SerializeField]
    private Color _inactiveColor = Color.black;
    [SerializeField]
    private TMP_Text _buttonText;

    public void SetUI(Color color, bool active, string buttonText)
    {
        ColorPanel.color = color;
        BackgroundPanel.color = active ? _activeColor : _inactiveColor;
        _buttonText.text = buttonText;
    }

    public void SetActive(bool active)
    {
        BackgroundPanel.color = active ? _activeColor : _inactiveColor;
    }

    public void SetEmpty()
    {
        ColorPanel.color = _inactiveColor;
        BackgroundPanel.color = _inactiveColor;
        _buttonText.text = "X";
    }
}
