using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : UnitAttack
{
    private EnemyUnit _enemyUnit;

    public float MeleeAttackDuration = 0.25f;
    public float MeleeAttackDelay = 0.5f;

    private float _timeMeleeAtkNext = 0;

    protected override void Awake()
    {
        base.Awake();
        _enemyUnit = GetComponent<EnemyUnit>();
        print(Damage);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null && Time.time >= _timeMeleeAtkNext && _enemyUnit.IsAlive)
        {
            print(Damage);
            damageable.ApplyDamage(Damage);
            _timeMeleeAtkNext = Time.time + MeleeAttackDelay;
            Debug.Log(gameObject.name + " наносит ему - " + other.name + " урон = " + Damage);
        }
    }
}
