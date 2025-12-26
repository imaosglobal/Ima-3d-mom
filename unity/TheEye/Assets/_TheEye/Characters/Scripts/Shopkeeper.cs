using UnityEngine;

/// <summary>
/// Shopkeeper - חנוני בכפר
/// </summary>
public class Shopkeeper : NPCCharacter
{
    [SerializeField] private InventoryItem[] itemsForSale;

    private void Start()
    {
        characterName = "Shopkeeper";
        dialogue = "Welcome to my shop! See anything you like?";
        canTrade = true;
        maxHealth = 50;
        currentHealth = maxHealth;
        InitializeShop();
    }

    private void InitializeShop()
    {
        itemsForSale = new InventoryItem[5];
        itemsForSale[0] = new InventoryItem { itemName = "Health Potion", type = ItemType.Consumable, quantity = 10 };
        itemsForSale[1] = new InventoryItem { itemName = "Mana Potion", type = ItemType.Consumable, quantity = 5 };
        itemsForSale[2] = new InventoryItem { itemName = "Iron Sword", type = ItemType.Weapon, quantity = 3 };
        itemsForSale[3] = new InventoryItem { itemName = "Leather Armor", type = ItemType.Armor, quantity = 2 };
        itemsForSale[4] = new InventoryItem { itemName = "Rope", type = ItemType.Miscellaneous, quantity = 20 };
        Debug.Log("[Shopkeeper] Shop initialized with 5 items");
    }

    public void SellItem(int index, PlayerCharacter buyer)
    {
        if (index >= 0 && index < itemsForSale.Length && itemsForSale[index] != null)
        {
            Debug.Log($"[Shopkeeper] Sold {itemsForSale[index].itemName} to {buyer.characterName}");
        }
    }

    public void ListItems()
    {
        Debug.Log("[Shopkeeper] Available items:");
        for (int i = 0; i < itemsForSale.Length; i++)
        {
            if (itemsForSale[i] != null)
            {
                Debug.Log($"  {i}: {itemsForSale[i].itemName} x{itemsForSale[i].quantity}");
            }
        }
    }
}
