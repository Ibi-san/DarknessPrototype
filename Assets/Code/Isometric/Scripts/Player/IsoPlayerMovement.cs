using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoPlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 1.5f;
    public float DashSpeed = 4f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    public LayerMask EnvironmentLayer;

    [SerializeField] private float _offsetDash = 0.2f;
    private float _dashEndTime;
    private float _lastDashTime;
    private bool _isDashing = false;

    private PlayerRenderer _isoRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isoRenderer = GetComponentInChildren<PlayerRenderer>();
    }


    void Update()
    {
        MovementPlayer();

        if (_isDashing && Time.time > _dashEndTime)
            _isDashing = false;

        if (Input.GetKeyDown(KeyCode.C) &&
            Time.time > _dashEndTime &&
            Time.time - _lastDashTime >= DashCooldown)
            Dash();
    }

    private void MovementPlayer()
    {
        _rigidbody.velocity = new Vector2(0, 0);

        Vector2 movement = GetDirection() * MovementSpeed;
        Vector2 newPos = _rigidbody.position + movement * Time.fixedDeltaTime;
        _isoRenderer.SetDirection(movement);
        _rigidbody.MovePosition(newPos);
    }

    private void Dash()
    {
        _isDashing = true;
        _dashEndTime = Time.time + DashDuration;
        _lastDashTime = Time.time;

        Vector2 dashDirection = GetDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, DashSpeed * DashDuration, EnvironmentLayer);

        Vector2 targetPosition;
        if (hit.collider != null)
        {
            targetPosition = hit.point - (dashDirection * _offsetDash);
        }
        else
        {
            targetPosition = _rigidbody.position + (dashDirection * DashSpeed * DashDuration);
        }

        _rigidbody.MovePosition(targetPosition);

    }

    private Vector2 GetDirection()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 directionVector = new Vector2(horizontalInput, verticalInput);
        directionVector = Vector2.ClampMagnitude(directionVector, 1);
        return directionVector;
    }
}
