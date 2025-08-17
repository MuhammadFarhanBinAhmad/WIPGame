using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]SO_Character SO_character;

    public int _BaseHealth;
    public int _Health;

    public int _BaseSpeed;
    public int _Speed;

    public void OnEnable()
    {
        _BaseHealth = SO_character._BaseHealth;
        _BaseSpeed = SO_character._BaseSpeed;

        _Health = _BaseHealth;
        _Speed = _BaseSpeed;
    }

    public void Start()
    {

    }

    public void TakeDamage(int dmg)
    {
        _Health -= dmg;

        if(_Health < 0 )
        {
            gameObject.SetActive(false);
        }
    }

}
