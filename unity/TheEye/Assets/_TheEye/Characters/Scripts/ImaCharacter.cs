using UnityEngine;

/// <summary>
/// ImaCharacter - דמות אמא עם משימות וטיפול רגשי
/// </summary>
public class ImaCharacter : NPCCharacter
{
    [SerializeField] private bool givingQuest = false;
    [SerializeField] private bool showingConcern = false;

    protected override void Start()
    {
        base.Start();
        characterName = "Ima (Mom)";
        maxHealth = 100;
        currentHealth = maxHealth;
        dialogue = "Be careful, my child...";
        canTrade = true; // אמא מסחרה בדברים חשובים
        Debug.Log("[Ima] Initialized - always watching over you");
    }

    /// <summary>
    /// אמא נותנת משימה על פי מצב השחקן
    /// </summary>
    public void GiveQuest(PlayerCharacter player)
    {
        if (player.GetHealth() < player.GetMaxHealth() / 2)
        {
            Debug.Log("[Ima] You look hurt! Please take this potion...");
            showingConcern = true;
        }
        else if (player.GetLevel() >= 5)
        {
            Debug.Log("[Ima] You've grown strong! Perhaps you're ready for a real challenge...");
            givingQuest = true;
        }
        else
        {
            Debug.Log("[Ima] Come back when you're stronger...");
        }
    }

    /// <summary>
    /// אמא משפיעה על רגשות השחקן
    /// </summary>
    public void ReactToPlayerAction(PlayerCharacterAction action)
    {
        switch (action)
        {
            case PlayerCharacterAction.Victory:
                Debug.Log("[Ima] I'm so proud of you!");
                break;
            case PlayerCharacterAction.Defeat:
                Debug.Log("[Ima] Don't give up! Try again!");
                break;
            case PlayerCharacterAction.LevelUp:
                Debug.Log("[Ima] You've grown! I can see it in your eyes...");
                break;
            case PlayerCharacterAction.DangerNearby:
                Debug.Log("[Ima] Be careful! I sense danger ahead!");
                break;
        }
    }

    /// <summary>
    /// אמא מחלקת פריטים חשובים
    /// </summary>
    public void GiveItem(PlayerCharacter player, InventoryItem item)
    {
        if (player.GetComponent<Inventory>()?.AddItem(item) == true)
        {
            Debug.Log($"[Ima] Here, take this: {item.itemName}");
        }
    }
}

public enum PlayerCharacterAction
{
    Victory,
    Defeat,
    LevelUp,
    DangerNearby,
    FarFromHome
}
