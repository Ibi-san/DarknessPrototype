using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class ProjectileEnemyTrigger : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private UnitEnemyTypeFlags _enemiesToDestroy;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyUnit enemyUnit))
        {
            if (!enemyUnit.Config.IsAlive())
                return;

            enemyUnit.ApplyDamage(_projectile.Damage);

            if (enemyUnit.Config.IsAlive())
            {
                OnEnemyIsAliveAfterAttack();
            }
            else
            {
                OnEnemyIsDeadAfterAttack(enemyUnit);
            }
        }
    }

    private void OnEnemyIsAliveAfterAttack()
    {
        _projectile.DisposeProjectile();
    }

    private void OnEnemyIsDeadAfterAttack(EnemyUnit enemyUnit)
    {
        if (_enemiesToDestroy.HasFlag((UnitEnemyTypeFlags)enemyUnit.EnemyUnitType))
        {
            Destroy(enemyUnit.gameObject);
        }
        else
        {
            _projectile.DisposeProjectile();
        }
    }
}
