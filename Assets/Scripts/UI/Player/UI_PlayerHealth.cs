using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerHealth : MonoBehaviour, I_UIObserver
{

    PlayerManager _PlayerManager;
    UIManager _UIManager;

    [Header("PlayerUI")]
    [SerializeField] Image _HealthBar;
    [SerializeField] TextMeshProUGUI _HealthText;

    public void UpdatePlayerUI()
    {
        _HealthText.text = _PlayerManager._Health.ToString() + " / " +_PlayerManager._BaseHealth.ToString();
        _HealthBar.fillAmount = (float)((float)_PlayerManager._Health / (float)_PlayerManager._BaseHealth);
    }

    void OnEnable()
    {
        _UIManager = FindAnyObjectByType<UIManager>();
        _PlayerManager = FindAnyObjectByType<PlayerManager>();

        _UIManager.AddObserver(this);
    }
    private void OnDisable()
    {
        _UIManager.RemoveObserver(this);
    }

}
