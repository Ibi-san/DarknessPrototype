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
        EditorUtility.SetDirty(target);
        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;

        _itemData.Type = (ItemType)EditorGUILayout.EnumPopup("ItemType", _itemData.Type);
        _itemData.Name = EditorGUILayout.TextField("Name", _itemData.Name);
        EditorGUILayout.LabelField("Description");
        _itemData.Description = EditorGUILayout.TextArea(_itemData.Description, style, GUILayout.MinHeight(80));
        _itemData.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", _itemData.Icon, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        switch (_itemData.Type)
        {
            case ItemType.Potion:
                break;
            case ItemType.Sword:
                _itemData.Damage = EditorGUILayout.IntField("Damage", _itemData.Damage);
                break;
            case ItemType.Bow:
                _itemData.Damage = EditorGUILayout.IntField("Damage", _itemData.Damage);
                break;
            case ItemType.Consumables:
                break;
            case ItemType.Resources:
                break;
            default:
                break;
        }
    }
}
#endif