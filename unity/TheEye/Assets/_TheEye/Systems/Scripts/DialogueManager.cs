using UnityEngine;

/// <summary>
/// DialogueManager - ניהול דיאלוגים עם NPCs
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private bool dialogueActive = false;
    private NPCCharacter currentNPC = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // בדוק לקלט דיאלוג
        if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            NextDialogueLine();
        }
    }

    /// <summary>
    /// התחלת דיאלוג עם NPC
    /// </summary>
    public void StartDialogue(NPCCharacter npc)
    {
        if (npc == null) return;

        currentNPC = npc;
        dialogueActive = true;

        Debug.Log($"[Dialogue] {npc.characterName}: Hello there!");
        
        if (npc is ImaCharacter ima)
        {
            ima.ReactToPlayerAction(PlayerCharacterAction.FarFromHome);
        }

        if (npc is QuestGiver qgiver)
        {
            // TODO: Display quest dialogue
        }
    }

    /// <summary>
    /// עבור לשורת הדיאלוג הבאה
    /// </summary>
    public void NextDialogueLine()
    {
        Debug.Log("[Dialogue] Next line");
        // TODO: Implement multi-line dialogue system
    }

    /// <summary>
    /// סגור דיאלוג
    /// </summary>
    public void EndDialogue()
    {
        dialogueActive = false;
        currentNPC = null;
        Debug.Log("[Dialogue] Dialogue ended");
    }

    public bool IsDialogueActive() => dialogueActive;
    public NPCCharacter GetCurrentNPC() => currentNPC;
}
