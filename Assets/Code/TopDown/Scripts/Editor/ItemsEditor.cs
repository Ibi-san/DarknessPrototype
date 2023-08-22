using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ItemData))]
public class ItemsEditor : Editor
{
    ItemData _itemData;
    // Start is called before the first frame update
    void OnEnable()
    {
        _itemData = (ItemData)target;
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        _itemData.Type = (ItemType)EditorGUILayout.EnumPopup("ItemType", _itemData.Type);
        switch (_itemData.Type)
        {
            case ItemType.Potion:
                _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
                _itemData.Description = EditorGUILayout.TextArea("Description", _itemData.Description);
                _itemData.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", _itemData.Icon, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                break;
            case ItemType.Sword:
                _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
                _itemData.Description = EditorGUILayout.TextArea("Description", _itemData.Description);
                _itemData.Damage = EditorGUILayout.IntField("Damage", _itemData.Damage);
                _itemData.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", _itemData.Icon, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                break;
            case ItemType.Bow:
                _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
                _itemData.Description = EditorGUILayout.TextArea("Description", _itemData.Description);
                _itemData.Damage = EditorGUILayout.IntField("Damage", _itemData.Damage);
                _itemData.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", _itemData.Icon, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                break;
            case ItemType.Consumables:
                _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
                _itemData.Description = EditorGUILayout.TextArea("Description", _itemData.Description);
                break;
            case ItemType.Resources:
                _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
                _itemData.Description = EditorGUILayout.TextArea("Description", _itemData.Description);
                break;
            default:
                break;
        }
    }
}
#endif