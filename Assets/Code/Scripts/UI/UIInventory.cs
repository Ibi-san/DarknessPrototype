using System;
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
            UIItem item = Instantiate(_itemPrefab, _contentPanel);
            _items.Add(item);
            item.OnItemClicked += HandleItemSelection;
        }
    }

    private void HandleItemSelection(UIItem item)
    {
        foreach (UIItem otherItems in _items)
        {
            otherItems.Deselect();
        }

        item.Select();
    }

    public void Show() 
    {
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);

    private void OnDisable()
    {
        foreach (var item in _items)
        {
            item.OnItemClicked -= HandleItemSelection;
        }
    }
}
