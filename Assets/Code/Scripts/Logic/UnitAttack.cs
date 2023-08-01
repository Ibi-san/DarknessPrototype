using System;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    public event Action<float> OnDamageChanged;
    public float Damage
    {
        get => _damage;
        set
        {
            _damage = Mathf.Clamp(value, 0f, float.MaxValue);
            OnDamageChanged?.Invoke(_damage);
        }
    }

    private float _damage;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (TryGetComponent(out Unit unit))
            _unit = unit;

        Damage = _unit.Config.Damage;
    }

    public void PerformAttack(IDamageable recipientDamagable)
    {
        recipientDamagable.ApplyDamage(Damage);
    }
}

