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
    }
    
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _playerStatus.PlayerHealth.CurrentHealth -= totalDamage;
        _playerStatus.PlayerInsanity.InsanityValue += _insanityRiseValue;
    }

    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}
