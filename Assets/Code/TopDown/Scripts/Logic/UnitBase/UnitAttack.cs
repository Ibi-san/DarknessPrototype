using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Logic;
using UnityEngine;

public class UnitAttack : MonoBehaviour, IModifier
{
    [SerializeField] private Unit _unit;

    public event Action<int> OnDamageChanged;

    public List<StatModifier> Modifiers = new();

    public int CurrentDamage
    {
        get => _currentDamage;
        private set
        {
            _currentDamage = Mathf.Clamp(value, 0, int.MaxValue);
            OnDamageChanged?.Invoke(_currentDamage);
        }
    }

    private int _currentDamage;

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (TryGetComponent(out Unit unit))
            _unit = unit;

        CurrentDamage = _unit.Config.Damage;
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier == null) return;
        Modifiers.Add(modifier);
        CurrentDamage += modifier.Value;
    }

    public void RemoveModifier<T>(T modifier)
    {
        if (modifier == null) return;
        
        var removeModifiers = Modifiers.Where(x => x.Source is T).ToList();
        
        foreach (var removeModifier in removeModifiers)
        {
            CurrentDamage -= removeModifier.Value;
            Modifiers.Remove(removeModifier);
        }
    }

    public void PerformAttack(IDamageable recipientDamageable)
    {
        recipientDamageable.ApplyDamage(CurrentDamage);
    }
}

