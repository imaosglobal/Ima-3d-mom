using UnityEngine;

/// <summary>
/// HealerCharacter - דמות מרפא
/// </summary>
public class HealerCharacter : PlayerCharacter
{
    [SerializeField] private int healingPower = 25;
    [SerializeField] private int manaPoints = 100;
    [SerializeField] private int maxMana = 100;

    private void Start()
    {
        characterName = "Healer";
        moveSpeed = 3.5f;
        maxHealth = 90;
        currentHealth = maxHealth;
        manaPoints = maxMana;
        Debug.Log($"[{characterName}] Healing Power: {healingPower}, Mana: {manaPoints}/{maxMana}");
    }

    private void Update()
    {
        base.Update();
        RegenerateMana();
    }

    private void RegenerateMana()
    {
        if (manaPoints < maxMana)
        {
            manaPoints += 0.8f * Time.deltaTime;
        }
    }

    public void HealSelf()
    {
        if (manaPoints >= 15)
        {
            manaPoints -= 15;
            currentHealth = Mathf.Min(currentHealth + healingPower, maxHealth);
            Debug.Log($"[{characterName}] Heals self for {healingPower}! Health: {currentHealth}/{maxHealth}");
        }
    }

    public void HealAlly(CharacterBase ally)
    {
        if (manaPoints >= 20)
        {
            manaPoints -= 20;
            // לוגיקת ריפוי של בן ברית
            Debug.Log($"[{characterName}] Heals {ally.name} for {healingPower}!");
        }
    }

    public void CastHolyLight()
    {
        if (manaPoints >= 30)
        {
            manaPoints -= 30;
            Debug.Log($"[{characterName}] Casts Holy Light! Mana: {manaPoints}");
            // לוגיקת כישוף ריפוי מתקדם
        }
    }
}
