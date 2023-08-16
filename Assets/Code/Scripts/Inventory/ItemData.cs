using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public ItemType Type;
    public int Damage;
    public Sprite Icon;
}

public enum ItemType
{
    Sword,
    Bow,
    Potion,
    Consumables,
    Resources
}