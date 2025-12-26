using UnityEngine;

/// <summary>
/// QuestGiver - מנהל משימות
/// </summary>
public class QuestGiver : NPCCharacter
{
    [SerializeField] private Quest questToGive;

    private void Start()
    {
        characterName = "Quest Master";
        dialogue = "I have a task for someone brave...";
        maxHealth = 60;
        currentHealth = maxHealth;
        InitializeQuest();
    }

    private void InitializeQuest()
    {
        questToGive = new Quest
        {
            questName = "Defeat the Forest Wolves",
            description = "The village has been plagued by wolves. Defeat 5 of them!",
            rewardXP = 200,
            rewardGold = 100,
            targetCount = 5
        };
        Debug.Log($"[Quest Master] Quest available: {questToGive.questName}");
    }

    public void OfferQuest(PlayerCharacter player)
    {
        if (player.GetComponent<QuestManager>())
        {
            var qm = player.GetComponent<QuestManager>();
            qm.AcceptQuest(questToGive);
            Debug.Log($"[Quest Master] Offered quest: {questToGive.questName}");
        }
    }

    public void ProgressQuest()
    {
        questToGive.currentCount++;
        Debug.Log($"[Quest Progress] {questToGive.currentCount}/{questToGive.targetCount}");
        if (questToGive.CheckCompletion())
        {
            Debug.Log($"[Quest Master] QUEST COMPLETED! Great job!");
        }
    }
}
