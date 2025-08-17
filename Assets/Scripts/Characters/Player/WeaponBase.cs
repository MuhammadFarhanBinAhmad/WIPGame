using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public SO_Weapon so_WeaponType;
    internal UIManager _UIManager;


    protected Camera _mainCam;
    protected ProjectilePool _projectilePool;

    public Transform _SpawnPosition;

    [Header("WeaponAmmo")]
    internal int magazine_BaseAmount;//Base magazine/clip size
    internal int magazine_CurrentAmount;//Current magazine/clip size
    internal int magazine_LeftAmount;//Ammo left in the player magazine/clip
    internal int ammoreserve_BaseAmount;//Base reserve amount
    internal int ammoreserve_CurrentAmount;//Reserve amount left

    [Header("WeaponReload")]
    internal float weapon_BaseReloadTime;//Base reload time
    internal float weapon_CurrentReloadTime;//Current time needed to reload weapon
    internal bool weapon_IsReloading;

    [Header("WeaponFireRate")]
    protected float _fireRate;
    protected float _nextTimeToFire;

    public virtual void Start()
    {
        _mainCam = Camera.main;
        _projectilePool = FindAnyObjectByType<ProjectilePool>();
        _UIManager = FindAnyObjectByType<UIManager>();
        StartState();
    }

    public virtual void StartState()
    {
        //Magazine start value
        magazine_CurrentAmount = magazine_BaseAmount;
        magazine_LeftAmount = magazine_CurrentAmount;
        //Ammo reserve start value
        ammoreserve_CurrentAmount = ammoreserve_BaseAmount;
        //Reload start value
        weapon_CurrentReloadTime = weapon_BaseReloadTime;
    }

    public abstract void ShootWeapon();

    /// <summary>
    /// Start to reload weapon
    /// </summary>
    public virtual void ReloadWeapon() 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!weapon_IsReloading)
            {
                weapon_IsReloading = true;
                StartCoroutine(ReloadingWeapon());
            }
        }

    }

    /// <summary>
    /// Weapon currently is reloading
    /// </summary>
    public virtual IEnumerator ReloadingWeapon()
    {
        yield return new WaitForSeconds(weapon_CurrentReloadTime);
        magazine_LeftAmount = magazine_CurrentAmount;
        ammoreserve_CurrentAmount -= magazine_CurrentAmount;
        _UIManager.UpdatePlayerUIObserver();
        weapon_IsReloading = false;
    }
}
