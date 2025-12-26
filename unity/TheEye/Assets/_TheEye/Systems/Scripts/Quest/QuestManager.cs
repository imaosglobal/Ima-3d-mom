using UnityEngine;

/// <summary>
/// QuestManager - מערכת משימות דינמית
/// </summary>
public class QuestManager : MonoBehaviour
{
    private Quest[] activeQuests = new Quest[10];
    private int completedQuestCount = 0;

    private void Start()
    {
        Debug.Log("[QuestManager] Initialized");
    }

    public void AcceptQuest(Quest quest)
    {
        for (int i = 0; i < activeQuests.Length; i++)
        {
            if (activeQuests[i] == null)
            {
                activeQuests[i] = quest;
                quest.isActive = true;
                Debug.Log($"[Quest] Accepted: {quest.questName} - {quest.description}");
                return;
            }
        }
        Debug.Log("[Quest] Too many active quests!");
    }

    public void CompleteQuest(Quest quest)
    {
        for (int i = 0; i < activeQuests.Length; i++)
        {
            if (activeQuests[i] == quest)
            {
                activeQuests[i].isActive = false;
                activeQuests[i] = null;
                completedQuestCount++;
                Debug.Log($"[Quest] Completed: {quest.questName}! +{quest.rewardXP} XP, +{quest.rewardGold} Gold");
                return;
            }
        }
    }

    public int GetCompletedQuestCount() => completedQuestCount;
    public int GetActiveQuestCount()
    {
        int count = 0;
        foreach (Quest q in activeQuests)
        {
            if (q != null && q.isActive) count++;
        }
        return count;
    }
}

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public int rewardXP = 100;
    public int rewardGold = 50;
    public bool isActive = false;
    public bool isCompleted = false;
    public int targetCount = 1;
    public int currentCount = 0;

    public bool CheckCompletion()
    {
        return currentCount >= targetCount;
    }
}
