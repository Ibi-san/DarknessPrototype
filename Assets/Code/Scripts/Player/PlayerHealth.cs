using System;
using Code.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerHealth : UnitHealth, IDamageable
{
    private PlayerStatus _playerStatus;

    private float _insanityRiseValue = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage(1);
            Debug.Log("Текущее здоровье: " + _playerStatus.PlayerHealth.CurrentHealth);
        }
        
        if(Input.GetKeyDown(KeyCode.U))
            Heal(1);
    }
    
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _playerStatus.PlayerHealth.CurrentHealth -= totalDamage;
        _playerStatus.PlayerInsanity.InsanityValue += _insanityRiseValue;
    }

    public void Heal(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _playerStatus.PlayerHealth.CurrentHealth += amount;
    }

    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
