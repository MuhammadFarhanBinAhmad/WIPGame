using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Scriptable Objects/PlayerWeapon")]
public class SO_Weapon : ScriptableObject
{
    [Header("WeaponStats")]
    public int magazine_BaseAmount;//Base magazine/clip size
    public int ammoreserve_BaseAmount;//Base reserve amount
    public float weapon_BaseReloadTime;//Base reload time
    public float weapon_fireRate;
    [Header("BulletStats")]
    public int ammo_damage;
    public int ammo_speed;

}
