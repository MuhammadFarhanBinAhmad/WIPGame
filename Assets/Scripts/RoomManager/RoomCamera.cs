using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField]
    GameObject _virtualcamera;
    CameraShake _CameraShake;

    private void OnEnable()
    {
        _CameraShake = FindAnyObjectByType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualcamera.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualcamera.SetActive(false);
        }
    }
}

