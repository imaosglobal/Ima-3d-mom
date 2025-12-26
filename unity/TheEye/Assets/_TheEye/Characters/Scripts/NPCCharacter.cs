using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NPCCharacter - דמות NPC עם משימות ודיאלוגים
/// </summary>
public class NPCCharacter : CharacterBase
{
    [SerializeField] private string dialogue = "Hello, traveler!";
    [SerializeField] private List<Quest> availableQuests = new List<Quest>();
    [SerializeField] private bool canTrade = false;

    private PlayerCharacter player;

    protected override void Start()
    {
        base.Start();
        maxHealth = 50; // NPCs חלשים יותר
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowDialogue();
        }
    }

    private void ShowDialogue()
    {
        Debug.Log($"[NPC: {characterName}] {dialogue}");
        
        if (availableQuests.Count > 0)
        {
            Debug.Log($"[{characterName}] Available quests: {availableQuests.Count}");
        }
        
        if (canTrade)
        {
            Debug.Log($"[{characterName}] Want to trade?");
        }
    }

    public void GiveQuest(Quest quest)
    {
        if (player != null && player.GetComponent<QuestManager>())
        {
            var questManager = player.GetComponent<QuestManager>();
            questManager.AcceptQuest(quest);
            Debug.Log($"[{characterName}] Gave quest: {quest.questName}");
        }
    }

    public void Trade()
    {
        if (canTrade)
        {
            Debug.Log($"[{characterName}] Opening trade menu...");
        }
    }
}
