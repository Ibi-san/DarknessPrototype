using System;
using Code.Scripts.Logic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IModifier
{
    [SerializeField] private Unit _unit;

    public event Action<int> OnHealthChanged;

    public int MaxHealth { get; private set; }

    public int CurrentHealth 
    { 
        get => _currentHealth; 
        set 
        { 
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        } 
    }

    private int _currentHealth;

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (TryGetComponent(out Unit unit))
            _unit = unit;

        MaxHealth = _unit.Config.Health;
        CurrentHealth = MaxHealth;
    }
}
