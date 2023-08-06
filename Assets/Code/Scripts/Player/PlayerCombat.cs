using Code.Scripts.Player;
using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerCombat : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private PlayerMovement _playerMovement;

    [Header("Set in Inspector")]
    [Header("Melee")]
    public float MeleeAttackRadius = 1f;
    public float MeleeAttackDuration = 0.25f;
    public float MeleeAttackDelay = 0.5f;

    public Transform MeleeAttackPosition;

    [Header("Projectile")]
    public float ProjectileAttackDuration = 0.25f;
    public float ProjectileAttackDelay = 0.5f;

    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private Projectile _projectilePrefab;
    //[SerializeField] private ForceMode2D _forceMode = ForceMode2D.Impulse;
    [SerializeField, Min(0f)] private float _force = 10f;

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
        _playerMovement = GetComponent<PlayerMovement>();
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
        GameObject test = Instantiate(_slashPrefab, MeleeAttackPosition);
        Destroy(test, 0.5f);
#endif


        int damagableLayer = LayerMask.NameToLayer("Damageable");
        int hitableLayer = LayerMask.NameToLayer("Hitable");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(MeleeAttackPosition.position, MeleeAttackRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == damagableLayer || hitCollider.gameObject.layer == hitableLayer)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.ApplyDamage(_playerStatus.PlayerAttack.Damage);
                    Debug.Log(gameObject.name + " наносит ему - " + hitCollider.name + " урон = " + _playerStatus.PlayerAttack.Damage);
                }
            }
        }
    }

    public void PerformAttackProjectile(Vector3 clickPos)
    {
        Vector2 direction = (clickPos - (Vector3)_weaponMuzzle.position).normalized;

        var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);
        //projectile.Rigidbody2D.velocity = direction * _force;
        //projectile.Rigidbody2D.AddForce(_weaponMuzzle.forward * _force, _forceMode);
        projectile.Rigidbody2D.DOMove(clickPos, 1).OnComplete(() => Destroy(projectile.gameObject));
        Debug.Log(_weaponMuzzle.forward + " - " + _force);
    }

    public void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

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
    }

    private IEnumerator DelayedEnablePlayerMovement()
    {
        yield return new WaitForSeconds(0.1f);
        _playerMovement.enabled = true;
    }
}
