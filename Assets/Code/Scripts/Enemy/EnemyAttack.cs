using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : UnitAttack
{

    public float MeleeAttackDuration = 0.25f;
    public float MeleeAttackDelay = 0.5f;

    private float _timeMeleeAtkNext = 0;

    private void OnTriggerStay2D(Collider2D other)
    {

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            if (Time.time >= _timeMeleeAtkNext)
            {
                damageable.ApplyDamage(Damage);
                _timeMeleeAtkNext = Time.time + MeleeAttackDelay;
                Debug.Log(gameObject.name + " наносит ему - " + other.name + " урон = " + Damage);
            }
        }
    }
}
