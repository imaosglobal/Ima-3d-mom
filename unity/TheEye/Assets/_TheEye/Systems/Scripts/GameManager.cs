using UnityEngine;

/// <summary>
/// GameManager - ניהול משחק כללי
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private bool isGameRunning = false;
    private PlayerCharacter player;
    private HUDManager hudManager;
    private QuestManager questManager;

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
        InitializeGame();
    }

    private void InitializeGame()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
        hudManager = HUDManager.Instance;
        questManager = player?.GetComponent<QuestManager>();

        if (player != null && questManager != null)
        {
            isGameRunning = true;
            Debug.Log("[GameManager] Game Initialized!");
            Debug.Log($"[GameManager] Player: {player.characterName} - {player.GetRole()}");
            Debug.Log("[GameManager] All systems ready!");
        }
        else
        {
            Debug.LogError("[GameManager] Failed to initialize game!");
        }
    }

    private void Update()
    {
        if (isGameRunning)
        {
            // בדיקות כלליות
            if (player != null && !player.IsAlive())
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        isGameRunning = false;
        Debug.Log("[GameManager] GAME OVER!");
        Time.timeScale = 0f; // עצור את המשחק
    }

    public bool IsGameRunning() => isGameRunning;
    public PlayerCharacter GetPlayer() => player;
}
