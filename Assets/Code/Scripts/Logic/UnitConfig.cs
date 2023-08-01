using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/Config", fileName = "UnitConfig", order = 0)]
public sealed class UnitConfig : ScriptableObject
{
    [Header("[Name]"), Space]

    [SerializeField] private string _unitName;

    [Header("[Common]"), Space]

    [SerializeField, Min(0)] private float _health;
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private float _speed;

    [Header("[Prefab]"), Space]

    [SerializeField] private Unit _unitPrefab;

    public string Name => _unitName;
    public float Health => _health;
    public float Damage => _damage;
    public float Speed => _speed;
}
