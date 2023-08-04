using Pathfinding;
using System;
using UnityEngine;

[RequireComponent(typeof(UnitHealth), typeof(UnitAttack))]
public class EnemyUnit : Unit, IDamageable
{
    [SerializeField] private UnitEnemyType _enemyUnitType;

    public GameObject EnemyAlive;
    public GameObject EnemyDead;
    public UnitEnemyType EnemyUnitType => _enemyUnitType;

    private UnitHealth _unitHealth;
    private UnitAttack _unitAttack;

    private AIPath _aiPath;
    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();
        _unitAttack = GetComponent<UnitAttack>();
        _aiPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        EnemyAlive.SetActive(true);
        EnemyDead.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(_unitHealth.Health);
        if (_unitHealth.Health == 0)
        {
            EnemyAlive.SetActive(false);
            _aiPath.enabled = false;
            EnemyDead.transform.position = transform.position;
            EnemyDead.transform.rotation = transform.rotation;
            EnemyDead.SetActive(true);
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