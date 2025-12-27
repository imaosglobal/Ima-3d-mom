using UnityEngine;

/// <summary>
/// GameInitializer - אתחול משחק שלם בהפעלה
/// </summary>
public class GameInitializer : MonoBehaviour
{
    [SerializeField] private CharacterRole selectedRole = CharacterRole.Creator;
    [SerializeField] private bool autoInitialize = true;
    [SerializeField] private string startSceneName = "Village";
    
    private static GameInitializer instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (autoInitialize)
        {
            InitializeNewGame();
        }
    }

    /// <summary>
    /// אתחול משחק חדש
    /// </summary>
    public void InitializeNewGame()
    {
        Debug.Log("[GameInitializer] Starting new game...");
        
        // יצור שחקן
        GameObject player = PrefabFactory.CreatePlayer(selectedRole, Vector3.zero);
        if (player == null)
        {
            Debug.LogError("[GameInitializer] Failed to create player!");
            return;
        }
        player.name = "Player";

        // יצור את אמא בכפר
        GameObject ima = PrefabFactory.CreateImaCharacter(new Vector3(2, 0, 2));
        if (ima == null)
        {
            Debug.LogError("[GameInitializer] Failed to create Ima!");
            return;
        }

        // אתחול GameManager
        if (GameManager.Instance == null)
        {
            GameObject gmObj = new GameObject("GameManager");
            gmObj.AddComponent<GameManager>();
        }

        // אתחול HUDManager
        if (HUDManager.Instance == null)
        {
            GameObject hudObj = new GameObject("HUDManager");
            hudObj.AddComponent<HUDManager>();
        }

        // אתחול AudioManager
        if (AudioManager.Instance == null)
        {
            GameObject audioObj = new GameObject("AudioManager");
            audioObj.AddComponent<AudioManager>();
        }

        Debug.Log($"[GameInitializer] Game initialized successfully!");
        Debug.Log($"[GameInitializer] Player: {selectedRole}");
        Debug.Log($"[GameInitializer] Starting in: {startSceneName}");
        Debug.Log("[GameInitializer] All systems ready!");
    }

    /// <summary>
    /// אתחול משחק מלחיצה
    /// </summary>
    public void ContinueGame()
    {
        Debug.Log("[GameInitializer] Continuing game from save...");
        // Attempt to load save via SaveManager
        if (SaveManager.Instance != null && SaveManager.Instance.HasSave())
        {
            SaveManager.Instance.LoadGame();
            Debug.Log("[GameInitializer] Save loaded and applied");
        }
        else
        {
            Debug.LogWarning("[GameInitializer] No save found, starting new game instead");
            InitializeNewGame();
        }
    }

    /// <summary>
    /// בחירת דמות
    /// </summary>
    public void SelectRole(CharacterRole role)
    {
        selectedRole = role;
        Debug.Log($"[GameInitializer] Role selected: {role}");
    }

    public static GameInitializer Instance => instance;
}
