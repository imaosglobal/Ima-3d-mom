using UnityEngine;

/// <summary>
/// ExplorerCharacter - דמות חוקרת
/// </summary>
public class ExplorerCharacter : PlayerCharacter
{
    [SerializeField] private int sensingRange = 20;
    [SerializeField] private int survivalSkill = 15;
    [SerializeField] private int trackingAbility = 12;

    private void Start()
    {
        characterName = "Explorer";
        moveSpeed = 6f;
        maxHealth = 85;
        currentHealth = maxHealth;
        Debug.Log($"[{characterName}] Sensing Range: {sensingRange}, Survival: {survivalSkill}, Tracking: {trackingAbility}");
    }

    public void SenseNearby()
    {
        Debug.Log($"[{characterName}] Senses nearby objects (Range: {sensingRange} units)");
        // לוגיקת חישה של פריטים וחברויות קרובות
    }

    public void TrackObject()
    {
        Debug.Log($"[{characterName}] Tracking object... (Ability: {trackingAbility})");
        // לוגיקת עקבול של דברים
    }

    public void GatherResources()
    {
        Debug.Log($"[{characterName}] Gathering resources... (Survival: {survivalSkill})");
        // לוגיקת איסוף משאבים
    }

    public void Scout()
    {
        Debug.Log($"[{characterName}] Scouting area... Detecting threats and treasures!");
        // לוגיקת סיור
    }
}
