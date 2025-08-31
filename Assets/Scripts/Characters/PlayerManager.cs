using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using FMOD.Studio;
public class PlayerManager : Character
{
    [Header("Jump and Ground Check Settings")]
    public float _jumpForce;
    public Transform _groundCheck;
    public float _groundCheckRadius;
    public LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    [SerializeField]
    private bool _isGrounded;

    [Header("Rotation Settings")]
    public float _spinSpeed; // degrees per second
    public float _grounddistance;
    public float base_timegroundcheck;
    public float current_timegroundcheck;
    int _spinDirection = 1;

    [Header("Dash Settings")]
    public float _dashForce;
    public float _dashDuration;
    public float _dashCooldown;
    float _dashCurrentCooldownTime;

    private bool _isDashing = false;

    [Header("Audio")]

    EventInstance sfx_PlayerFootStep;


    public bool GetIsDashing() { return _isDashing; }
    public void SetIsDashinag(bool dashing) { _isDashing = dashing; }

    private new void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        base.Start();

        sfx_PlayerFootStep = AudioManager.Instance.CreateEventInstance(FmodEvent.Instance.sfx_PlayerFootStep);
    }

    void Update()
    {
        Movement();
        HandleRotation();
        StartDash();
        UpdateSound();
    }


    void Movement()
    {
        if (!_isDashing)
        {
            // Movement
            float horizontal = Input.GetAxis("Horizontal");
            _rigidbody.linearVelocity = new Vector2(horizontal * _Speed, _rigidbody.linearVelocity.y);
        }

        // Ground check
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        // Jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }


    }

 void HandleRotation()
    {
        if (IsNearGround())
        {
            // Smoothly rotate back to upright (0,0,0)
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            // Spin in air
            transform.Rotate(Vector3.forward * _spinSpeed * Time.deltaTime);
        }
        if (_isGrounded)
        {
            current_timegroundcheck = base_timegroundcheck;
        }
    }
    bool IsNearGround()
    {
        if(!_isGrounded && current_timegroundcheck > 0)
        {
            current_timegroundcheck -= Time.deltaTime;
            return false;
        }
        else
        {
            Vector2 position = transform.position;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, _grounddistance, _groundLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }
    void OnDrawGizmosSelected()
    {
        if (_groundCheck == null) return;

        Vector2 pos = transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos, pos + Vector2.down * _grounddistance);
    }
    void StartDash()
    {
        if (_isDashing)
        {
            CurrentlyDashing();
            return;
        }


        if (_dashCurrentCooldownTime > 0)
        {
            _dashCurrentCooldownTime -= Time.deltaTime;
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && _dashCurrentCooldownTime <= 0)
        {
            _isDashing = true;
            _dashCurrentCooldownTime = _dashCooldown;
            _rigidbody.linearVelocity = Vector2.zero;
            AudioManager.Instance.PlayOneShot(FmodEvent.Instance.sfx_PlayerDashing,transform.position);
            StartCoroutine(Dashing());
        }
    }
    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(_dashDuration);
        CancelInvoke("CurrentlyDashing");
        _rigidbody.linearVelocity = Vector2.zero;
        _isDashing = false;
    }

    void CurrentlyDashing()
    {
        float dir = Input.GetAxis("Horizontal");
        _rigidbody.linearVelocity = new Vector2(dir * _dashForce, _rigidbody.linearVelocity.y);
    }
    void UpdateSound()
    {
        if(_rigidbody.linearVelocityX != 0 && _isGrounded)
        {
            PLAYBACK_STATE playbackstate;
            sfx_PlayerFootStep.getPlaybackState(out playbackstate);

            if (playbackstate.Equals(PLAYBACK_STATE.STOPPED))
            {
                sfx_PlayerFootStep.start();
            }
        }
        else
        {
            sfx_PlayerFootStep.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
