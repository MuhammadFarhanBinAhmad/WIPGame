using UnityEngine;

public enum BULLETOWNER
{
    PLAYER,
    ENEMY
}

public class Projectiles : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField]TrailRenderer _trailRenderer;

    Vector2 _direction;

    internal BULLETOWNER _BulletOwner;
    int _dmg, _speed;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Invoke("SelfDestruct", 3);
    }
    public void SetDamage(int dmg) { _dmg = dmg; }
    public int GetDamage() { return _dmg; }

    public void SetSpeed(int speed) { _speed = speed; }
    public int GetSpeed() { return _speed; }

    public void SetDirection(Vector2 dir) { _direction = dir; }
    public void SetPosition(Vector2 pos) { transform.position = pos; }

    public void SetOwner(BULLETOWNER owner) { _BulletOwner = owner; }

    public BULLETOWNER GetOwner() { return _BulletOwner; }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _direction * _speed;
    }


    internal void SelfDestruct()
    {
        _trailRenderer.Clear();
        gameObject.SetActive(false);
    }
}
