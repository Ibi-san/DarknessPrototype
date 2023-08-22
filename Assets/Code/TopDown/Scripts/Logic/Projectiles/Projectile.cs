using DG.Tweening;
using UnityEngine;


[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [Header("Common")]
    [SerializeField, Min(0f)] private int _damage = 1;
    [SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.OnAnyCollision;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody2D _projectileRigidbody;

    [Header("Effect On Destroy")]
    [SerializeField] private bool _spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem _effectOnDestroyPrefab;
    [SerializeField, Min(0f)] private float _effectOnDestroyLifetime = 2f;

    public bool IsProjectileDisposed { get; private set; }
    public int Damage => _damage;
    public ProjectileDisposeType DisposeType => _disposeType;
    public Rigidbody2D Rigidbody2D => _projectileRigidbody;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (IsProjectileDisposed)
            return;

        if (collision2D.gameObject.TryGetComponent(out IDamageable damageable))
        {
            OnTargetCollision(collision2D, damageable);

            if (_disposeType == ProjectileDisposeType.OnTargetCollision)
                DisposeProjectile();
        }
        else
            OnOtherCollision(collision2D);

        OnAnyCollision(collision2D);

        if (_disposeType == ProjectileDisposeType.OnAnyCollision)
            DisposeProjectile();
    }

    public void DisposeProjectile()
    {
        OnProjectileDispose();
        SpawnEffectOnDestroy();
        _projectileRigidbody.DOKill();
        Destroy(gameObject);
        IsProjectileDisposed = true;
    }

    private void SpawnEffectOnDestroy()
    {
        if (_spawnEffectOnDestroy == false)
            return;

        var effect = Instantiate(_effectOnDestroyPrefab, transform.position, _effectOnDestroyPrefab.transform.rotation);

        Destroy(effect.gameObject, _effectOnDestroyLifetime);
    }

    protected virtual void OnProjectileDispose() { }
    protected virtual void OnAnyCollision(Collision2D collision2D) { }
    protected virtual void OnOtherCollision(Collision2D collision2D) { }
    protected virtual void OnTargetCollision(Collision2D collision2D, IDamageable damageable) { }
}
