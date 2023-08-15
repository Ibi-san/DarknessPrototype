using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIItem _itemPrefab;
    [SerializeField] private RectTransform _contentPanel;

    private List<UIItem> _items = new List<UIItem>();

    public void InitializeInventoryUI(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIItem item = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(_contentPanel);
            _items.Add(item);
        }
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
}
