using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public enum STATE
    {
        PATROLLING,
        ATTACKING
    }

    [SerializeField]protected GameObject _target;
    protected Rigidbody2D _rigidbody;


    STATE _state;


    [SerializeField] LayerMask _detectionMask;
    [Header("Combat Stats")]
    protected float _currentSpeed;
    protected float _stopDistance;
    protected float _viewRange;
    protected float _attackRate;
    protected float _nextAttack;
    protected int _currentHealth;

    [Header("Patrol Stats")]
    [SerializeField] Transform[] _patrolPosition = new Transform[2];
    protected int _currentPatrolIndex = 0;
    protected float _waitTimer = 0f;
    protected float _waitDuration = 3f;


    public event Action<BaseEnemy> OnEnemyDied;
    public event Action<BaseEnemy> OnEnemyHit;

    private void OnEnable()
    {
        _target = FindAnyObjectByType<PlayerManager>().gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();

        SetValue();

        _state = STATE.PATROLLING;

        FindAnyObjectByType<Player_Combo>().AddEnemyToComboCountList(this);

    }

    protected virtual void SetValue(){}
    private void Update()
    {

        switch (_state)
        {
            case STATE.PATROLLING:
                {
                    Patrolling();
                    DetectPlayer();
                    break;
                }
            case STATE.ATTACKING:
                {
                    ChasePlayer();
                    break;
                }
        }
    }

    void Patrolling()
    {
        if (_patrolPosition.Length < 2) return; // Needs 2 points

        Transform targetPoint = _patrolPosition[_currentPatrolIndex];
        float distance = Vector2.Distance(transform.position, targetPoint.position);

        if (distance <= 0.1f)
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y); // stop moving

            // Reached patrol point -> idle
            if (_waitTimer <= 0f)
            {
                _waitTimer = _waitDuration; // start wait
            }
            else
            {
                _waitTimer -= Time.deltaTime;
                if (_waitTimer <= 0f)
                {
                    // Switch to next patrol point
                    _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPosition.Length;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }

            }
        }
        else
        {
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            _rigidbody.linearVelocity = new Vector2(direction.x * _currentSpeed, _rigidbody.linearVelocity.y);
        }
    }
    void DetectPlayer()
    {
        Vector2 facingDir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDir, _viewRange, _detectionMask);

        Debug.DrawRay(transform.position, facingDir * _viewRange, Color.red); // Debug line in Scene view
        if (hit)
            _state = STATE.ATTACKING;
    }
    public void ChasePlayer()
    {
        if (_target == null)
            return;

        Vector2 enemyPos = transform.position;
        Vector2 targetPos = _target.transform.position;

        float distance = Vector3.Distance(transform.position, _target.transform.position);

        if (Mathf.Abs(distance) >= _stopDistance)
        {
            Vector2 direction = (targetPos - enemyPos).normalized;

            // move only on X axis
            _rigidbody.linearVelocity = new Vector2(direction.x * _currentSpeed, _rigidbody.linearVelocity.y);
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y); // stop when attacking
            AttackPlayer();
        }
    }

    public virtual void AttackPlayer(){}

    void TakeDamage(int dmg)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dmg;
            _state = STATE.ATTACKING;
            OnEnemyHit?.Invoke(this);
        }
        else
        {
            OnEnemyDied?.Invoke(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<RoomManager>() != null)
        {
            RoomManager _roomManager = other.GetComponent<RoomManager>();
            _roomManager.AddEnemyToList(this);

        }
        if (other.GetComponent<Projectiles>() != null)
        {
            Projectiles temp_Proj = other.GetComponent<Projectiles>();
            if(temp_Proj._BulletOwner == BULLETOWNER.PLAYER)
            {
                TakeDamage(temp_Proj.GetDamage());
                temp_Proj.SelfDestruct();
            }

            AudioManager.Instance.PlayOneShot(FmodEvent.Instance.sfx_EnemyHit, this.transform.position);

        }
    }

}
