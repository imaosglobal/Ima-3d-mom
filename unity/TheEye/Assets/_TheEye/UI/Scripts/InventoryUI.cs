using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventoryUI - ממשק תיק
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private Text itemDetailsText;
    
    private Inventory playerInventory;

    private void Start()
    {
        Debug.Log("[InventoryUI] Inventory system ready");
    }

    public void RefreshInventory(Inventory inv)
    {
        playerInventory = inv;
        // ניתן להוסיף לוגיקה לעדכון התצוגה
        Debug.Log("[InventoryUI] Inventory refreshed");
    }

    public void ShowItemDetails(InventoryItem item)
    {
        if (itemDetailsText != null && item != null)
        {
            itemDetailsText.text = $"{item.itemName}\nType: {item.type}\nQuantity: {item.quantity}";
        }
    }
}
