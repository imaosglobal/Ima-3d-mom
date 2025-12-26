using UnityEngine;

/// <summary>
/// HunterCharacter - דמות צייד
/// </summary>
public class HunterCharacter : PlayerCharacter
{
    [SerializeField] private int accuracy = 85;
    [SerializeField] private int criticalChance = 15;
    [SerializeField] private string weaponType = "Bow";

    private void Start()
    {
        characterName = "Hunter";
        moveSpeed = 5.5f;
        maxHealth = 80;
        currentHealth = maxHealth;
        Debug.Log($"[{characterName}] Accuracy: {accuracy}%, Critical Chance: {criticalChance}%, Weapon: {weaponType}");
    }

    public void ShootArrow()
    {
        int damage = Random.value < criticalChance / 100f ? 40 : 20;
        Debug.Log($"[{characterName}] Shoots arrow for {damage} damage!");
        // לוגיקת ירי
    }

    public void TrackPrey()
    {
        Debug.Log($"[{characterName}] Tracking prey... (Accuracy: {accuracy}%)");
        // לוגיקת עקבול
    }
}
