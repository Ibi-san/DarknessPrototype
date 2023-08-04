using System;
using UnityEngine;

[RequireComponent (typeof(UnitHealth), typeof(PlayerInsanity))]
public class PlayerUnit : Unit, IDamageable
{
    private UnitHealth _unitHealth;
    private PlayerInsanity _playerInsanity;

    private float _insanityRiseValue = 0.5f;

    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        
        _playerInsanity = GetComponent<PlayerInsanity>();
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
        _playerInsanity.InsanityValue += _insanityRiseValue;
    }

    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
