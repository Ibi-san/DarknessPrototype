using Pathfinding;
using System;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class EnemyUnit : Unit, IDamageable
{
    [SerializeField] private UnitEnemyType _enemyUnitType;

    public GameObject EnemyAlive;
    public GameObject EnemyDead;
    public UnitEnemyType EnemyUnitType => _enemyUnitType;

    public bool IsAlive { get; private set; }

    private UnitHealth _unitHealth;

    private AIPath _aiPath;
    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _aiPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        IsAlive = true;
        EnemyAlive.SetActive(true);
        EnemyDead.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        _unitHealth.Health -= totalDamage;
        
        if (_unitHealth.Health == 0)
            Die();
    }

    private void Die()
    {
        IsAlive = false;
        EnemyAlive.SetActive(false);
        _aiPath.enabled = false;
        EnemyDead.transform.position = transform.position;
        EnemyDead.transform.rotation = transform.rotation;
        EnemyDead.SetActive(true);
    }
    
    private int ProcessDamage(int damage)
    {
        return damage; //добавить мультипликаторы урона
    }
}