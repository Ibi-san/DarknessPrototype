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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage(1);
            Debug.Log("Текущее здоровье: " + _unitHealth.Health);
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
