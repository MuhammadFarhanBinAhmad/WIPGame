using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Combo", menuName = "Scriptable Objects/SO_Combo")]

[System.Serializable]
public class ComboLevelData
{
    public int requisite;          // Combo count needed
    public string levelName;       // Name text
    public Color color;
    public int sizePercent = 100;        // Size percent for first letter
}

public class SO_Combo : ScriptableObject
{
    public List<ComboLevelData> levels = new List<ComboLevelData>();

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
