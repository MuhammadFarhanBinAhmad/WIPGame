using UnityEngine;

public class UIManager : UISubject
{

    public void RegisterEnemy(BaseEnemy enemy)
    {
        enemy.OnEnemyDied += UpdateEnemyCount;
    }

    public void UpdateEnemyCount(BaseEnemy enemy)
    {
        print("enemy down");
    }

    void Start()
    {
        UpdatePlayerUIObserver();
    }



}
