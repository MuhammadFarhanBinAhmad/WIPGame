using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<BaseEnemy> _EnemyList = new List<BaseEnemy> ();
    [SerializeField]internal CameraShake _camShake;
    bool _isRoomCleared;

    [SerializeField]GameObject _roomDoor;

    public void AddEnemyToList(BaseEnemy _enemy)
    {
        _EnemyList.Add (_enemy);
        _enemy.OnEnemyDied += OnEnemyDeath;
        _camShake.AddCamShakeOnDeathEvent(_enemy);
        _camShake.RemoveCamShakeOnDeathEvent(_enemy);
    }

    public void OnEnemyDeath(BaseEnemy _enemy)
    {
        if (!_EnemyList.Contains(_enemy))
            return;
        
        _enemy.gameObject.SetActive (false);
        _EnemyList.Remove(_enemy);

        if (_isRoomCleared)
            { return; }

        if(_EnemyList.Count <= 0)
        {
            _isRoomCleared = true;
            _roomDoor.SetActive(false);
        }
    }


}
