using UnityEngine;

/// <summary>
/// EventManager - ניהול אירועים במשחק
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    // Events
    public delegate void GameEvent();
    public delegate void CharacterEvent(CharacterBase character);
    public delegate void QuestEvent(Quest quest);

    // Game Events
    public event GameEvent OnGameStarted;
    public event GameEvent OnGamePaused;
    public event GameEvent OnGameResumed;
    public event GameEvent OnGameEnded;

    // Character Events
    public event CharacterEvent OnCharacterLevelUp;
    public event CharacterEvent OnCharacterDied;
    public event CharacterEvent OnCharacterDamaged;
    public event CharacterEvent OnCharacterHealed;

    // Quest Events
    public event QuestEvent OnQuestStarted;
    public event QuestEvent OnQuestCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("[EventManager] Initialized");
    }

    // Game Event Triggers
    public void TriggerGameStarted()
    {
        Debug.Log("[EventManager] Game Started");
        OnGameStarted?.Invoke();
    }

    public void TriggerGamePaused()
    {
        Debug.Log("[EventManager] Game Paused");
        OnGamePaused?.Invoke();
    }

    public void TriggerGameResumed()
    {
        Debug.Log("[EventManager] Game Resumed");
        OnGameResumed?.Invoke();
    }

    public void TriggerGameEnded()
    {
        Debug.Log("[EventManager] Game Ended");
        OnGameEnded?.Invoke();
    }

    // Character Event Triggers
    public void TriggerCharacterLevelUp(CharacterBase character)
    {
        Debug.Log($"[EventManager] {character.characterName} leveled up!");
        OnCharacterLevelUp?.Invoke(character);
    }

    public void TriggerCharacterDied(CharacterBase character)
    {
        Debug.Log($"[EventManager] {character.characterName} died!");
        OnCharacterDied?.Invoke(character);
    }

    public void TriggerCharacterDamaged(CharacterBase character)
    {
        OnCharacterDamaged?.Invoke(character);
    }

    public void TriggerCharacterHealed(CharacterBase character)
    {
        Debug.Log($"[EventManager] {character.characterName} was healed!");
        OnCharacterHealed?.Invoke(character);
    }

    // Quest Event Triggers
    public void TriggerQuestStarted(Quest quest)
    {
        Debug.Log($"[EventManager] Quest started: {quest.questName}");
        OnQuestStarted?.Invoke(quest);
    }

    public void TriggerQuestCompleted(Quest quest)
    {
        Debug.Log($"[EventManager] Quest completed: {quest.questName}");
        OnQuestCompleted?.Invoke(quest);
    }
}
