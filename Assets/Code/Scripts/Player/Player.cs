using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Set in Inspector")]
    public float Speed = 5;
    public float AttackDuration = 0.25f;
    public float AttackDelay = 0.5f;
    public float DashSpeed = 50f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;


    private float _timeAtkDone = 0;
    private float _timeAtkNext = 0;
    private float _dashEndTime;
    private float _lastDashTime;
    private bool _isDashing = false;

    private Rigidbody2D _rigid;
    private Animator _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();
        _rigid.velocity = Speed * dir;

        if (_isDashing && Time.time > _dashEndTime)
        {
            _isDashing = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && Time.time > _dashEndTime && Time.time - _lastDashTime >= DashCooldown)
        {
            Dash();
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= _timeAtkNext)
        {
            _timeAtkDone = Time.time + AttackDuration;
            _timeAtkNext = Time.time + AttackDelay;
        }

    }

    private void Dash()
    {
        _isDashing = true;
        _dashEndTime = Time.time + DashDuration;
        _lastDashTime = Time.time;


        Vector2 dashDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            dashDirection.x = -1;
        else if (Input.GetKey(KeyCode.D))
            dashDirection.x = 1;
        if (Input.GetKey(KeyCode.W))
            dashDirection.y = 1;
        else if (Input.GetKey(KeyCode.S))
            dashDirection.y = -1;

        dashDirection.Normalize();
        
        Vector2 targetPosition = _rigid.position + (dashDirection * DashSpeed * DashDuration);
        _rigid.MovePosition(targetPosition);
    }
}
