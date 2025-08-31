using UnityEngine;
using FMODUnity;

public class FmodEvent : MonoBehaviour
{
    [Header("Environment")]
    [field: Header("Coin sfx")]
    [field: SerializeField] public EventReference sfx_GunShot { get; private set; }

    [Header("Player")]
    [field: Header("Player Footstep sfx")]
    [field: SerializeField] public EventReference sfx_PlayerFootStep { get; private set; }    
    [field: Header("Player Dashing sfx")]
    [field: SerializeField] public EventReference sfx_PlayerDashing { get; private set; }

    [Header("Enemy")]
    [field: Header("Enemy Hit sfx")]
    [field: SerializeField] public EventReference sfx_EnemyHit { get; private set; }


    public static FmodEvent Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null)
            print("more than one Fmod Event instance in the scene");

        Instance = this;
    }
}
