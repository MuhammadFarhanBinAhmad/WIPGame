using UnityEngine;

[CreateAssetMenu(fileName = "RangeEnemyStats", menuName = "Scriptable Objects/RangeEnemy")]
public class SO_RangeEnemy : ScriptableObject
{
    public float _baseSpeed;
    public int _baseHealth;
    public float _attackRate;
    public float _stopDistance;
    public float _viewrange;
    public float _waitDuration;
    public int _damage;
    public int _speed;
}
