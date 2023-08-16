using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _qunatityText;
    
    [SerializeField] private Image _borderImage;

    public event Action<UIItem> OnItemClicked;

    private bool _empty = true;
    private ItemData _itemData;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        _itemIcon.gameObject.SetActive(false);
        _empty = true;
    }

    public void Deselect()
    {
        _borderImage.enabled = false;
    }

    public void SetData(ItemData itemData, int quantity)
    {
        _itemData = itemData;
        _itemIcon.gameObject.SetActive(true);
        _itemIcon.sprite = itemData.Icon;
        _qunatityText.text = quantity.ToString();
        _empty = false;
    }

    public void SetData(ItemData itemData)
    {
        _itemData = itemData;
        _itemIcon.gameObject.SetActive(true);
        _itemIcon.sprite = itemData.Icon;
        _qunatityText.transform.parent.gameObject.SetActive(false);
        _empty = false;
    }

    public void Select()
    {
        _borderImage.enabled = true;
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        if(pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if (_empty) return;
        ItemTooltip.Instance.SetItemData(_itemData);
        ItemTooltip.Instance.ShowTooltip();
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        ItemTooltip.Instance.HideTooltip();
    }
}
