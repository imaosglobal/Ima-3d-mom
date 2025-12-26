using UnityEngine;

/// <summary>
/// Blacksmith - נפח
/// </summary>
public class Blacksmith : NPCCharacter
{
    [SerializeField] private int upgradeCost = 50;

    private void Start()
    {
        characterName = "Blacksmith";
        dialogue = "I forge the finest weapons!";
        canTrade = true;
        maxHealth = 70;
        currentHealth = maxHealth;
        Debug.Log("[Blacksmith] Forge is ready!");
    }

    public void UpgradeWeapon(PlayerCharacter player)
    {
        if (player.GetGold() >= upgradeCost)
        {
            player.AddGold(-upgradeCost);
            Debug.Log($"[Blacksmith] Upgraded {player.characterName}'s weapon! Cost: {upgradeCost} gold");
        }
        else
        {
            Debug.Log("[Blacksmith] Not enough gold for upgrade!");
        }
    }

    public void ForgeNewWeapon(string weaponName)
    {
        Debug.Log($"[Blacksmith] Forging {weaponName}... *CLANGING SOUNDS*");
        // לוגיקת יצור נשק
    }

    public void RepairArmor()
    {
        Debug.Log("[Blacksmith] Repairing your armor to perfect condition!");
    }
}
