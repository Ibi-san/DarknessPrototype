using System;
using UnityEngine;

[RequireComponent (typeof(UnitHealth), typeof(UnitAttack))]
public class PlayerUnit : Unit, IDamageable
{
    public float AttackRadius = 1f;
    public float AttackDuration = 0.25f;
    public float AttackDelay = 0.5f;

    public Transform AttackPosition;
    
    private float _timeAtkDone = 0;
    private float _timeAtkNext = 0;

    private UnitHealth _unitHealth;
    private UnitAttack _unitAttack;
    
    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _unitAttack = GetComponent<UnitAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage(1);
            Debug.Log("Текущее здоровье: " + _unitHealth.Health);
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= _timeAtkNext)
        {
            PerformAttack();
            _timeAtkDone = Time.time + AttackDuration;
            _timeAtkNext = Time.time + AttackDelay;
        }
    }

    private void PerformAttack()
    {
        int damagableLayer = LayerMask.NameToLayer("Damagable");
        int hitableLayer = LayerMask.NameToLayer("Hitable");

        LayerMask layerMask = (1 << damagableLayer) | (1 << hitableLayer);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(AttackPosition.position, AttackRadius);

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

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _unitHealth.Health -= totalDamage;
    }

    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
