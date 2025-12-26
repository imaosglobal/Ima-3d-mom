using UnityEngine;

/// <summary>
/// Inventory - מערכת תיק המטבח
/// </summary>
public class Inventory : MonoBehaviour
{
    public int maxSlots = 20;
    private InventoryItem[] items;
    
    private void Start()
    {
        items = new InventoryItem[maxSlots];
        Debug.Log("[Inventory] Initialized with " + maxSlots + " slots");
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                Debug.Log($"[Inventory] Added: {item.itemName}");
                return true;
            }
        }
        Debug.Log("[Inventory] Full!");
        return false;
    }

    public bool RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < items.Length && items[slotIndex] != null)
        {
            Debug.Log($"[Inventory] Removed: {items[slotIndex].itemName}");
            items[slotIndex] = null;
            return true;
        }
        return false;
    }

    public InventoryItem GetItem(int slotIndex)
    {
        return slotIndex >= 0 && slotIndex < items.Length ? items[slotIndex] : null;
    }
}

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity = 1;
    public ItemType type;
}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Quest,
    Miscellaneous
}
