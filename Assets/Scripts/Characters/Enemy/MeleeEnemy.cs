using UnityEngine;

public class MeleeEnemy : BaseEnemy
{

    [SerializeField]SO_MeleeEnemy _MeleeEnemy;

    protected override void SetValue()
    {
        _currentSpeed = _MeleeEnemy._baseSpeed;
        _currentHealth = _MeleeEnemy._baseHealth;
        _stopDistance = _MeleeEnemy._stopDistance;
        _viewRange = _MeleeEnemy._viewrange;
        _attackRate = _MeleeEnemy._attackRate;
        _waitDuration = _MeleeEnemy._waitDuration;
    }

    public override void AttackPlayer()
    {
        if (Time.time >= _nextAttack)
        {
            Debug.Log("attack player"); _nextAttack = Time.time + _attackRate; // schedule next attack }
        }
    }
}
