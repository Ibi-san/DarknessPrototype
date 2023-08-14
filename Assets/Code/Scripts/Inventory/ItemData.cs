using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public ItemType Type;
    public int Damage;
    public Sprite Icon;
}

public enum ItemType
{
    Sword,
    Bow,
    Potion,

}