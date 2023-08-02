using System;
using UnityEngine;

[RequireComponent(typeof(UnitHealth), typeof(UnitAttack))]
public class EnemyUnit : Unit, IDamageable
{
    [SerializeField] private UnitEnemyType _enemyUnitType;

    public UnitEnemyType EnemyUnitType => _enemyUnitType;

    private UnitHealth _unitHealth;
    private UnitAttack _unitAttack;

    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _unitAttack = GetComponent<UnitAttack>();
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