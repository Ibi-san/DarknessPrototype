using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public static ItemTooltip Instance { get; private set; }

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemTypeText;
    public TextMeshProUGUI ItemDamageText;
    public Image ItemIconImage;
    public CanvasGroup TooltipCanvasGroup;

    private ItemData _currentItemData;
    private RectTransform _tooltipRectTransform;

    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _tooltipRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Vector2 cursorPosition = Input.mousePosition;
            Vector2 offset = new Vector2(xOffset, yOffset);
            var delta = cursorPosition - offset;
            _tooltipRectTransform.position = delta;
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    ShowTooltip();
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    HideTooltip();
    //}

    public void ShowTooltip()
    {
        if (_currentItemData == null) return;
        UpdateTooltip();
        TooltipCanvasGroup.alpha = 1;
    }

    public void HideTooltip()
    {
        TooltipCanvasGroup.alpha = 0;
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