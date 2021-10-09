using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiVisualConfig", menuName = "Vandal/AiVisualConfig")]
public class AiVisualConfig : ScriptableObject
{
    public List<AiVisual> aiVisuals;
}

[Serializable]
public class AiVisual
{
    public GameObject VisualPrefab;
}
