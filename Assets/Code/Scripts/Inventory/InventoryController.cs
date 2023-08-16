using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public ItemData[] Invent;

    [SerializeField] private UIInventory _inventoryUI;
    [SerializeField] private int _inventorySize = 10;

    private void Start()
    {
        _inventoryUI.InitializeInventoryUI(_inventorySize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.Show();
            }
            else
            {
                _inventoryUI.Hide();
            }
        }
    }
}
