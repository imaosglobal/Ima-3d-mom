using UnityEngine;

/// <summary>
/// MageCharacter - דמות קוסמת
/// </summary>
public class MageCharacter : PlayerCharacter
{
    [SerializeField] private int manaPoints = 100;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int spellPower = 30;

    private void Start()
    {
        characterName = "Mage";
        moveSpeed = 4f;
        maxHealth = 70;
        currentHealth = maxHealth;
        manaPoints = maxMana;
        Debug.Log($"[{characterName}] Mana: {manaPoints}/{maxMana}, Spell Power: {spellPower}");
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
            manaPoints += 1 * Time.deltaTime;
        }
    }

    public void CastFireball()
    {
        if (manaPoints >= 20)
        {
            manaPoints -= 20;
            Debug.Log($"[{characterName}] Casts Fireball for {spellPower} damage! Mana: {manaPoints}");
            // לוגיקת כישוף
        }
        else
        {
            Debug.Log($"[{characterName}] Not enough mana!");
        }
    }

    public void CastFrostNova()
    {
        if (manaPoints >= 25)
        {
            manaPoints -= 25;
            Debug.Log($"[{characterName}] Casts Frost Nova! Mana: {manaPoints}");
            // לוגיקת כישוף
        }
    }
}
