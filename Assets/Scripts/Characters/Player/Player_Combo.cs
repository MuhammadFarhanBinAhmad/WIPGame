using UnityEngine;

public class Player_Combo : MonoBehaviour
{
    int combo_Count;


    public static Player_Combo Instance { get; private set; }


    private void OnEnable()
    {
        Instance = this;
    }

    public void AddEnemyToComboCountList(BaseEnemy enemy)
    {
        enemy.OnEnemyHit += IncreaseComboCount;
    }

    public void IncreaseComboCount(BaseEnemy enemy)
    {
        combo_Count++;
        UI_PlayerCombo.Instance.ChangeComboNumberLevelText(combo_Count);
    }
    public void ResetComboCount()
    {
        
    }
}
