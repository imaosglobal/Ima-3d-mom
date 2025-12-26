using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HUDManager - ממשק משתמש הראשי
/// </summary>
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [SerializeField] private Text healthText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text questText;
    [SerializeField] private Slider healthBar;

    private PlayerCharacter player;
    private QuestManager questManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
        questManager = player?.GetComponent<QuestManager>();
        Debug.Log("[HUDManager] Initialized");
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // בריאות
        if (healthBar != null)
        {
            healthBar.value = (float)player.GetHealth() / player.GetMaxHealth();
        }
        if (healthText != null)
        {
            healthText.text = $"HP: {player.GetHealth()}/{player.GetMaxHealth()}";
        }

        // רמה
        if (levelText != null)
        {
            levelText.text = $"Level: {player.GetLevel()}";
        }

        // זהב
        if (goldText != null)
        {
            goldText.text = $"Gold: {player.GetGold()}";
        }

        // משימות
        if (questText != null && questManager != null)
        {
            questText.text = $"Quests: {questManager.GetActiveQuestCount()}";
        }
    }

    public void ShowMessage(string message)
    {
        Debug.Log($"[HUD] {message}");
    }
}
