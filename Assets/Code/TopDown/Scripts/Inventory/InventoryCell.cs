using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData ItemData;
    public GameObject TooltipPrefab;
    private GameObject _currentTooltip;


    private void Update()
    {
        if (_currentTooltip != null)
        {
            Vector2 tooltipPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
            _currentTooltip.transform.position = tooltipPosition;
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
        if (_currentTooltip == null)
        {
            _currentTooltip = Instantiate(TooltipPrefab, transform);
            var tooltip = _currentTooltip.GetComponent<ItemTooltip>();
            tooltip.SetItemData(ItemData);

            Vector2 tooltipPosition = new Vector2(transform.position.x, transform.position.y + 50);
            tooltip.transform.position = tooltipPosition;
        }
    }

    public void HideTooltip()
    {
        if (_currentTooltip != null)
        {
            Destroy(_currentTooltip);
        }
    }
}