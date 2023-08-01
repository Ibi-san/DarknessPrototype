using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    public event Action<float> OnHealthChanged;

    public float MaxHealth { get; private set; }

    public float Health 
    { 
        get => _health; 
        set 
        { 
            _health = Mathf.Clamp(value, 0f, MaxHealth);
            OnHealthChanged?.Invoke(_health);
        } 
    }

    private float _health;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (TryGetComponent(out Unit unit))
            _unit = unit;

        MaxHealth = _unit.Config.Health;
        Health = MaxHealth;
    }
}
