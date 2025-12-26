using UnityEngine;

/// <summary>
/// Doctor - רופא בכפר
/// </summary>
public class Doctor : NPCCharacter
{
    private void Start()
    {
        characterName = "Doctor";
        dialogue = "Do you need medical attention?";
        canTrade = true;
        maxHealth = 50;
        currentHealth = maxHealth;
        Debug.Log("[Doctor] Ready to heal!");
    }

    public void HealPlayer(PlayerCharacter patient, int healAmount = 50)
    {
        patient.AddExperience(10); // קצת בונוס XP
        Debug.Log($"[Doctor] Healed {patient.characterName} for {healAmount} HP");
    }

    public void CureDisease()
    {
        Debug.Log("[Doctor] Curing disease... You feel much better!");
    }

    public void OfferAdvice()
    {
        Debug.Log("[Doctor] Remember to rest well and eat healthy food!");
    }
}
