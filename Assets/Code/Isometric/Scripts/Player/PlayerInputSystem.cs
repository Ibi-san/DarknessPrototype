using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    public float MovementSpeed = 1.5f;
    public float DashSpeed = 4f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    public LayerMask EnvironmentLayer;


    [SerializeField] private float _offsetDash = 0.2f;

    [SerializeField] private InputActionReference _movementInput;
    [SerializeField] private InputActionReference _dashInput;


    private float _dashEndTime;
    private float _lastDashTime;
    private bool _isDashing = false;

    private PlayerRenderer _isoRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isoRenderer = GetComponentInChildren<PlayerRenderer>();

        _dashInput.action.performed += OnDash;
    }

    private void OnDestroy()
    {
        _dashInput.action.performed -= OnDash;

    }

    private void Update()
    {
        MovementPlayer();


    }

    private void MovementPlayer()
    {

        Vector2 axis = _movementInput.action.ReadValue<Vector2>();
        Vector2 movement = axis * MovementSpeed;
        Vector2 newPos = _rigidbody.position + movement * Time.fixedDeltaTime;
        _isoRenderer.SetDirection(movement);
        _rigidbody.MovePosition(newPos);
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

            _isDashing = true;
            _dashEndTime = Time.time + DashDuration;
            _lastDashTime = Time.time;


            Vector2 dashDirection = _movementInput.action.ReadValue<Vector2>();
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
            Debug.Log("C");
        }

    }

}
