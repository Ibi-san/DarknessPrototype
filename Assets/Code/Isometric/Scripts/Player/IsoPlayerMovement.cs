using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoPlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 1.5f;
    public float DashSpeed = 4f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;



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
        Vector2 currentPos = _rigidbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * MovementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        _isoRenderer.SetDirection(movement);
        _rigidbody.MovePosition(newPos);
    }

    private void Dash()
    {
        _isDashing = true;
        _dashEndTime = Time.time + DashDuration;
        _lastDashTime = Time.time;

        Vector2 currentPos = _rigidbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * MovementSpeed;
        Vector2 newPos = currentPos + (movement * DashSpeed * DashDuration);
        _isoRenderer.SetDirection(movement);
        _rigidbody.MovePosition(newPos);
    }
}
