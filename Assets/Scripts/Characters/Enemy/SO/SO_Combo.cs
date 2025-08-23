using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Combo", menuName = "Scriptable Objects/SO_Combo")]
public class SO_Combo : ScriptableObject
{
    public List<int> _comboLevelRequisite = new List<int>();
    public List<string> _comboLevelName = new List<string>();

    public float text_duration; // Total time for bump
    [Header("Level")]
    public float level_sizeStart;
    public float level_sizePeak;
    public float level_sizeEnd;
    [Header("Counter")]
    public float counter_sizeStart;
    public float counter_sizePeak;
    public float counter_sizeEnd;

}
