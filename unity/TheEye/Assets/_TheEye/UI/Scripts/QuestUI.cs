using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// QuestUI - ממשק משימות
/// </summary>
public class QuestUI : MonoBehaviour
{
    [SerializeField] private Text questListText;
    [SerializeField] private Text questDetailsText;
    [SerializeField] private Button acceptBtn;
    [SerializeField] private Button abandonBtn;

    private QuestManager questManager;

    private void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("Player")?.GetComponent<QuestManager>();
        Debug.Log("[QuestUI] Quest UI initialized");
    }

    public void DisplayQuests()
    {
        if (questManager == null) return;

        int activeQuests = questManager.GetActiveQuestCount();
        int completedQuests = questManager.GetCompletedQuestCount();

        if (questListText != null)
        {
            questListText.text = $"Active: {activeQuests}\nCompleted: {completedQuests}";
        }
        Debug.Log($"[QuestUI] Active Quests: {activeQuests}, Completed: {completedQuests}");
    }

    public void ShowQuestDetails(Quest quest)
    {
        if (questDetailsText != null && quest != null)
        {
            questDetailsText.text = $"{quest.questName}\n{quest.description}\n" +
                                   $"Progress: {quest.currentCount}/{quest.targetCount}\n" +
                                   $"Reward: {quest.rewardXP} XP, {quest.rewardGold} Gold";
        }
    }
}
