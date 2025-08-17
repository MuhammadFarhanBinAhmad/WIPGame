using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [Tooltip("Maximum positional offset (in units).")]
    public float maxPositionOffset = 0.5f;

    [Tooltip("Maximum angle (in degrees) for rotation shake.")]
    public float maxRotationAngle = 10f;

    [Tooltip("How quickly trauma decays per second.")]
    public float traumaDecay = 1f;

    [Tooltip("Power factor to control non-linear shake response.")]
    [Range(1f, 3f)] public float traumaPower = 2f;

    [Tooltip("Frequency of shake movement.")]
    [SerializeField] float frequency;
    [SerializeField] float _traumaValue;
    float trauma;

    private float seedX, seedY, seedRotX, seedRotY;
    private CinemachineCamera cineCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private Vector3 originalLocalPos;
    private Quaternion originalLocalRot;



    private void Awake()
    {
        cineCamera = GetComponent<CinemachineCamera>();
        noise = cineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localRotation;

        // Seeds for variation
        seedX = Random.Range(0f, 100f);
        seedY = Random.Range(0f, 100f);
        seedRotX = Random.Range(0f, 100f);
        seedRotY = Random.Range(0f, 100f);
    }
    //note to self, remove magic number. make it a passsable param value
    public void AddCamShakeEvent(BaseEnemy enemy)
    {
        enemy.OnEnemyDied += (e) => SetTrauma(e, 1f);
    }
    public void RemoveCamShakeEvent(BaseEnemy enemy)
    {
        enemy.OnEnemyDied -= (e) => SetTrauma(e, 1f);
    }
    private void SetTrauma(BaseEnemy enemy, float value) => trauma = Mathf.Clamp01(value);

    private void Update()
    {
        if (cineCamera == null || noise == null) return;

        if (trauma > 0f)
        {
            float shake = Mathf.Pow(trauma, traumaPower);
            float time = Time.time * frequency;

            // Perlin noise offsets
            float offsetX = (Mathf.PerlinNoise(seedX, time) - 0.5f) * 2f;
            float offsetY = (Mathf.PerlinNoise(seedY, time) - 0.5f) * 2f;
            float offsetRotZ = (Mathf.PerlinNoise(seedRotX, time) - 0.5f) * 2f;
            float offsetRotY = (Mathf.PerlinNoise(seedRotY, time) - 0.5f) * 2f;

            // Cinemachine noise (layered shake)
            noise.AmplitudeGain = maxPositionOffset * shake * Mathf.Max(Mathf.Abs(offsetX), Mathf.Abs(offsetY));
            noise.FrequencyGain = frequency * shake;

            cineCamera.Lens.Dutch = maxRotationAngle * shake * offsetRotZ;

            // Extra transform-based shake
            transform.localPosition = originalLocalPos +
                                      new Vector3(offsetX, offsetY, 0f) * maxPositionOffset * shake;

            transform.localRotation = originalLocalRot *
                                      Quaternion.Euler(offsetRotY * maxRotationAngle * shake,
                                                       0f,
                                                       offsetRotZ * maxRotationAngle * shake);

            // Decay trauma
            trauma = Mathf.Clamp01(trauma - traumaDecay * Time.deltaTime);
        }
        else
        {
            // Reset when no shake
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0f;
            cineCamera.Lens.Dutch = 0f;

            transform.localPosition = originalLocalPos;
            transform.localRotation = originalLocalRot;
        }
    }

    public void AddTrauma(float amount) => trauma = Mathf.Clamp01(trauma + Mathf.Abs(amount));
    public void SetTrauma(float value) => trauma = Mathf.Clamp01(value);
    public float GetTrauma() => trauma;
}
