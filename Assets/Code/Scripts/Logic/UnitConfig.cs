using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/Config", fileName = "UnitConfig", order = 0)]
public sealed class UnitConfig : ScriptableObject
{
    [Header("[Name]"), Space]

    [SerializeField] private string _unitName;

    [Header("[Common]"), Space]

    [SerializeField, Min(0)] private int _health;
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private int _speed;

    [Header("[Prefab]"), Space]

    [SerializeField] private Unit _unitPrefab;

    public string Name => _unitName;
    public int Health => _health;
    public int Damage => _damage;
    public int Speed => _speed;
}
