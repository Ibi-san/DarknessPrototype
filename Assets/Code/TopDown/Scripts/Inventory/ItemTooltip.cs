using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemTypeText;
    public TextMeshProUGUI ItemDamageText;
    public Image ItemIconImage;

    private ItemData _currentItemData;
    private RectTransform _tooltipRectTransform;

    private void Awake()
    {
        _tooltipRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Vector2 cursorPosition = Input.mousePosition;
            _tooltipRectTransform.position = cursorPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    public void ShowTooltip()
    {
        UpdateTooltip();
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTooltip()
    {
        ItemNameText.text = _currentItemData.Name;
        ItemTypeText.text = _currentItemData.Type.ToString();
        ItemDamageText.text = "Damage: " + _currentItemData.Damage;
        ItemIconImage.sprite = _currentItemData.Icon;
    }

    public void SetItemData(ItemData itemData)
    {
        _currentItemData = itemData;
        UpdateTooltip();
    }
}