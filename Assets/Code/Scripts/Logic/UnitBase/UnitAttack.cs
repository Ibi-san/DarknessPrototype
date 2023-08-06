using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Logic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
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

    public void PerformAttack(IDamageable recipientDamageable)
    {
        recipientDamageable.ApplyDamage(CurrentDamage);
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier == null) return;
        Modifiers.Add(modifier);
        Modifiers.ForEach(x=> CurrentDamage += x.Value);

    }

    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier == null) return;
        Modifiers.Remove(modifier);
        Modifiers.ForEach(x=> CurrentDamage += x.Value);
    }

    public void RemoveModifier<T>(T type)
    {
        if (type == null) return;
        // var statModifier = Modifiers.FirstOrDefault(x => x.Source.GetType() == typeof(T));
        // print(statModifier);
        // Modifiers.Remove(statModifier);
        Modifiers.ForEach(x=> CurrentDamage -= x.Value);

    }
}

