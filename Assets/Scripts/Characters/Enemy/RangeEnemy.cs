using UnityEngine;

public class RangeEnemy : BaseEnemy
{

    [SerializeField]SO_RangeEnemy _rangeEnemy;

    ProjectilePool _pool;
    [SerializeField] Transform proj_SpawnPos;

    [SerializeField] Transform _weapon;

    private void Start()
    {
        _pool = FindAnyObjectByType<ProjectilePool>();
    }

    protected override void SetValue()
    {
        _currentSpeed = _rangeEnemy._baseSpeed;
        _currentHealth = _rangeEnemy._baseHealth;
        _stopDistance = _rangeEnemy._stopDistance;
        _viewRange = _rangeEnemy._viewrange;
        _attackRate = _rangeEnemy._attackRate;
        _waitDuration = _rangeEnemy._waitDuration;
    }

    public override void AttackPlayer()
    {
        _weapon.right = _target.transform.position - transform.position;

        if (Time.time >= _nextAttack)
        {
            GameObject GO_proj = _pool.GetProjectile();
            Projectiles _proj = GO_proj.GetComponent<Projectiles>();

            Vector2 direction = (_target.transform.position - proj_SpawnPos.position).normalized;

            _proj.SetOwner(BULLETOWNER.ENEMY);
            _proj.SetDirection(direction);
            _proj.SetPosition(proj_SpawnPos.position);
            _proj.SetDamage(_rangeEnemy._damage);
            _proj.SetSpeed(_rangeEnemy._speed);

            _nextAttack = Time.time + _attackRate; // schedule next attack }
        }
    }

}
