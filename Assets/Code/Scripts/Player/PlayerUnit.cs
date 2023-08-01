using System;
using UnityEngine;

[RequireComponent (typeof(UnitHealth), typeof(UnitAttack))]
public class PlayerUnit : Unit, IDamageable
{
    private UnitHealth _unitHealth;
    private UnitAttack _unitAttack;

    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _unitAttack = GetComponent<UnitAttack>();
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _unitHealth.Health -= totalDamage;
    }

    private float ProcessDamage(float damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
