using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float Speed = 5;
    
    public float DashSpeed = 50f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;


    
    private float _dashEndTime;
    private float _lastDashTime;
    private bool _isDashing = false;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private Camera _mainCamera;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        LookAtMouse();

        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            dir.x = -1;
        else if (Input.GetKey(KeyCode.D))
            dir.x = 1;

        if (Input.GetKey(KeyCode.W))
            dir.y = 1;
        else if (Input.GetKey(KeyCode.S))
            dir.y = -1;

        dir.Normalize();
        _rigid.velocity = Speed * dir;

        if (_isDashing && Time.time > _dashEndTime)
            _isDashing = false;

        if (Input.GetKeyDown(KeyCode.C) && 
            Time.time > _dashEndTime && 
            Time.time - _lastDashTime >= DashCooldown)
            Dash();
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

    private void LookAtMouse()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = _mainCamera.transform.position.z;
        transform.LookAt(mousePos, Vector3.back);
    }
}
