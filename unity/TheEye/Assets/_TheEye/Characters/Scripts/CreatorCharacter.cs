using UnityEngine;

/// <summary>
/// CreatorCharacter - דמות בונה/יוצרת
/// </summary>
public class CreatorCharacter : PlayerCharacter
{
    [SerializeField] private int buildingPower = 20;
    [SerializeField] private int craftingLevel = 1;

    private void Start()
    {
        characterName = "Creator";
        moveSpeed = 4f;
        maxHealth = 100;
        currentHealth = maxHealth;
        Debug.Log($"[{characterName}] Building Power: {buildingPower}, Crafting Level: {craftingLevel}");
    }

    public void CraftItem(string itemName)
    {
        Debug.Log($"[{characterName}] Crafting {itemName}...");
        // לוגיקת הכנת פריטים
    }

    public void BuildStructure(string structureName)
    {
        Debug.Log($"[{characterName}] Building {structureName}... (Power: {buildingPower})");
        // לוגיקת בנייה
    }
}
