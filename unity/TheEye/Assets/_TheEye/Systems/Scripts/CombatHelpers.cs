using UnityEngine;

/// <summary>
/// CombatHelper - עזרים ללוגיקת קרב
/// </summary>
public static class CombatHelper
{
    /// <summary>
    /// חישוב נזק מבוסס סטטיסטיקות
    /// </summary>
    public static int CalculateDamageWithModifiers(
        int baseDamage,
        float strengthModifier = 1f,
        float weaponMultiplier = 1f,
        float criticalMultiplier = 1f)
    {
        return Mathf.RoundToInt(baseDamage * strengthModifier * weaponMultiplier * criticalMultiplier);
    }

    /// <summary>
    /// בדיקה אם זה היט קריטי
    /// </summary>
    public static bool RollCritical(int criticalChance)
    {
        return Random.Range(0, 100) < criticalChance;
    }

    /// <summary>
    /// חישוב חיסכון נזק
    /// </summary>
    public static int CalculateArmor(int incomingDamage, int armorRating)
    {
        float reduction = (float)armorRating / 100f;
        return Mathf.RoundToInt(incomingDamage * (1f - reduction));
    }

    /// <summary>
    /// בדיקה אם זה "hit" או "miss"
    /// </summary>
    public static bool RollHit(int accuracy)
    {
        return Random.Range(0, 100) < accuracy;
    }

    /// <summary>
    /// חישוב XP מהנצחון על אויב
    /// </summary>
    public static int GetEnemyXP(int enemyLevel, int playerLevel)
    {
        int baseXP = enemyLevel * 50;
        float levelDifference = (enemyLevel - playerLevel) * 0.1f;
        return Mathf.Max(10, Mathf.RoundToInt(baseXP * (1 + levelDifference)));
    }

    /// <summary>
    /// חישוב תגמול זהב
    /// </summary>
    public static int GetEnemyGold(int enemyLevel)
    {
        return Random.Range(enemyLevel * 5, enemyLevel * 15);
    }
}

/// <summary>
/// HealthHelper - עזרים לנהול בריאות
/// </summary>
public static class HealthHelper
{
    /// <summary>
    /// חישוב הרפואה בסיסי
    /// </summary>
    public static int CalculateHealing(int baseHealing, float skillModifier = 1f)
    {
        return Mathf.RoundToInt(baseHealing * skillModifier);
    }

    /// <summary>
    /// בדוק אם דמות מתה
    /// </summary>
    public static bool IsDead(int currentHealth)
    {
        return currentHealth <= 0;
    }

    /// <summary>
    /// בדוק אם דמות בחלוש (נמוך HP)
    /// </summary>
    public static bool IsLowHealth(int currentHealth, int maxHealth)
    {
        return (float)currentHealth / maxHealth < 0.3f;
    }

    /// <summary>
    /// חזור לבריאות מלאה
    /// </summary>
    public static int RestoreFullHealth(int maxHealth)
    {
        return maxHealth;
    }
}
