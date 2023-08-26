using Code.Scripts.Player;
using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerCombat : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private IsoPlayerMovement _playerMovement;
    private PlayerRenderer _isoRenderer;

    [Header("Set in Inspector")]
    [Header("Melee")]
    [SerializeField] private Transform _meleeAttackPosition;

    public float MeleeAttackRadius = 1f;
    public float MeleeAttackDuration = 0.25f;
    public float MeleeAttackDelay = 0.5f;


    [Header("Projectile")]
    public float ProjectileAttackDuration = 0.25f;
    public float ProjectileAttackDelay = 0.5f;

    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField, Min(0f)] private float _arrowSpeed = 10f;

    private bool _isAttacking = false;

    private float _timeMeleeAtkDone = 0;
    private float _timeMeleeAtkNext = 0;

    private float _timeProjectileAtkDone = 0;
    private float _timeProjectileAtkNext = 0;

    [Header("EditorTest")]
    [SerializeField] private GameObject _slashPrefab;

    private void Awake()
    {
        _playerStatus = GetComponent<PlayerStatus>();
        _playerMovement = GetComponent<IsoPlayerMovement>();
        _isoRenderer = GetComponentInChildren<PlayerRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= _timeMeleeAtkNext)
        {
            PerformAttackMelee();
            _timeMeleeAtkDone = Time.time + MeleeAttackDuration;
            _timeMeleeAtkNext = Time.time + MeleeAttackDelay;
        }

        if (Input.GetMouseButtonDown(1) && Time.time >= _timeProjectileAtkNext)
        {
            if (!_isAttacking)
            {
                _isAttacking = true;
                _playerMovement.enabled = false;
                RotatePlayer();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                PerformAttackProjectile(mousePos);
                _timeProjectileAtkDone = Time.time + ProjectileAttackDuration;
                _timeProjectileAtkNext = Time.time + ProjectileAttackDelay;
                StartCoroutine(DelayedEnablePlayerMovement());
                _isAttacking = false;
            }
        }
    }

    private void PerformAttackMelee()
    {
#if UNITY_EDITOR
        GameObject test = Instantiate(_slashPrefab, _meleeAttackPosition);
        Destroy(test, 0.5f);
#endif
        _isoRenderer.SetDirection(GetDirection());

        int damagableLayer = LayerMask.NameToLayer("Damageable");
        int hitableLayer = LayerMask.NameToLayer("Hitable");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_meleeAttackPosition.position, MeleeAttackRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == damagableLayer || hitCollider.gameObject.layer == hitableLayer)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.ApplyDamage(_playerStatus.PlayerAttack.CurrentDamage);
                    Debug.Log(gameObject.name + " наносит ему - " + hitCollider.name + " урон = " + _playerStatus.PlayerAttack.CurrentDamage);
                }
            }
        }
    }

    private void PerformAttackProjectile(Vector3 clickPos)
    {

        var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);
        projectile.transform.LookAt(clickPos, Vector3.back);
        projectile.Rigidbody2D.DOMove(clickPos, _arrowSpeed).SetSpeedBased().OnComplete(() => Destroy(projectile.gameObject));

    }

    private void RotatePlayer()
    {
        //Vector2 direction = GetDirection();
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

        _meleeAttackPosition.rotation = targetRotation;
        _weaponMuzzle.rotation = targetRotation;

        _isoRenderer.SetDirection(direction);
        /*

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle >= -45f && angle < 45f)
        {
            // Право
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (angle >= 45f && angle < 135f)
        {
            // Верх
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (angle >= -135f && angle < -45f)
        {
            // Низ
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            // Лево
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        */
    }

    private Vector2 GetDirection()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 directionVector = new Vector2(horizontalInput, verticalInput);
        directionVector = Vector2.ClampMagnitude(directionVector, 1);
        return directionVector;
    }


    private IEnumerator DelayedEnablePlayerMovement()
    {
        yield return new WaitForSeconds(0.1f);
        _playerMovement.enabled = true;
    }
}
