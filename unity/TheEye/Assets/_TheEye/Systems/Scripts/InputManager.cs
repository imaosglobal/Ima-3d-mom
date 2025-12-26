using UnityEngine;

/// <summary>
/// InputManager - ניהול קלט משחקן
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerCharacter currentPlayer;

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
        currentPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        if (currentPlayer == null) return;

        HandleGameplayInput();
        HandleUIInput();
        HandleDebugInput();
    }

    private void HandleGameplayInput()
    {
        // תנועה - טופלת בPlayer Character עצמו
        // אבל אנחנו יכולים להוסיף כאן input handlers נוספים
    }

    private void HandleUIInput()
    {
        // I - Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("[InputManager] Inventory toggled");
        }

        // Q - Quests
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("[InputManager] Quests toggled");
        }

        // M - Map
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("[InputManager] Map toggled");
        }

        // P - Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        // ESC - Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }
    }

    private void HandleDebugInput()
    {
#if UNITY_EDITOR
        // Debug: Add XP
        if (Input.GetKeyDown(KeyCode.F1))
        {
            currentPlayer?.AddExperience(100);
            Debug.Log("[InputManager] +100 XP (Debug)");
        }

        // Debug: Add Gold
        if (Input.GetKeyDown(KeyCode.F2))
        {
            currentPlayer?.AddGold(100);
            Debug.Log("[InputManager] +100 Gold (Debug)");
        }

        // Debug: Damage self
        if (Input.GetKeyDown(KeyCode.F3))
        {
            currentPlayer?.TakeDamage(10);
            Debug.Log("[InputManager] -10 Health (Debug)");
        }

        // Debug: Level up
        if (Input.GetKeyDown(KeyCode.F4))
        {
            currentPlayer?.AddExperience(1000);
            Debug.Log("[InputManager] Force Level Up (Debug)");
        }

        // Debug: List all enemies
        if (Input.GetKeyDown(KeyCode.F5))
        {
            var enemies = FindObjectsOfType<EnemyCharacter>();
            Debug.Log($"[InputManager] Enemies found: {enemies.Length}");
            foreach (var enemy in enemies)
            {
                Debug.Log($"  - {enemy.characterName}: HP={enemy.GetHealth()}/{enemy.GetMaxHealth()}");
            }
        }
#endif
    }

    private void TogglePause()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
        Debug.Log($"[InputManager] Game paused: {Time.timeScale == 0f}");
    }

    private void OpenMainMenu()
    {
        Debug.Log("[InputManager] Opening main menu...");
        if (SceneController.Instance != null)
        {
            SceneController.Instance.LoadMainMenu();
        }
    }
}
