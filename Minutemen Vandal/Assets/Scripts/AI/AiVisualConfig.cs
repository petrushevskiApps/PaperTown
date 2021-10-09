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
    public string walkTrigger = "walk";
    public string walkState = "Walking";
    public string runTrigger = "run";
    public string runState = "Running";
    public string idleTrigger = "idle";
    public string idleState = "Idle";
}
