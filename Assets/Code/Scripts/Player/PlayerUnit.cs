using System;
using UnityEngine;

[RequireComponent (
    typeof(UnitHealth), 
    typeof(UnitAttack),
    typeof(PlayerInsanity))]
public class PlayerUnit : Unit, IDamageable
{
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
    [SerializeField] private ForceMode2D _forceMode = ForceMode2D.Impulse;
    [SerializeField, Min(0f)] private float _force = 10f;

    
    private float _timeMeleeAtkDone = 0;
    private float _timeMeleeAtkNext = 0;

    private float _timeProjectileAtkDone = 0;
    private float _timeProjectileAtkNext = 0;

    private float _insanityRiseValue = 0.5f;

    private UnitHealth _unitHealth;
    private UnitAttack _unitAttack;
    private PlayerInsanity _playerInsanity;
    
    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _unitAttack = GetComponent<UnitAttack>();
        _playerInsanity = GetComponent<PlayerInsanity>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage(1);
            Debug.Log("Текущее здоровье: " + _unitHealth.Health);
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= _timeMeleeAtkNext)
        {
            PerformAttackMelee();
            _timeMeleeAtkDone = Time.time + MeleeAttackDuration;
            _timeMeleeAtkNext = Time.time + MeleeAttackDelay;
        }

        if (Input.GetMouseButtonDown(1) && Time.time >= _timeProjectileAtkNext)
        {
            PerformAttackProjectile();
            _timeProjectileAtkDone = Time.time + ProjectileAttackDuration;
            _timeProjectileAtkNext = Time.time + ProjectileAttackDelay;
        }
    }

    private void PerformAttackMelee()
    {
        int damagableLayer = LayerMask.NameToLayer("Damageable");
        int hitableLayer = LayerMask.NameToLayer("Hitable");

        LayerMask layerMask = (1 << damagableLayer) | (1 << hitableLayer);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(MeleeAttackPosition.position, MeleeAttackRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == damagableLayer || hitCollider.gameObject.layer == hitableLayer)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.ApplyDamage(_unitAttack.Damage);
                    Debug.Log(gameObject.name + " наносит ему - " + hitCollider.name + " урон = " + _unitAttack.Damage);
                }
            }
        }
    }

    public void PerformAttackProjectile()
    {
        var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);

        projectile.Rigidbody2D.AddForce(_weaponMuzzle.forward * _force, _forceMode);
        Debug.Log(_weaponMuzzle.forward + " - " + _force);
    }
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _unitHealth.Health -= totalDamage;
        _playerInsanity.InsanityValue += _insanityRiseValue;
    }

    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
