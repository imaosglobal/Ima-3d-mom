using UnityEngine;
using System.IO;

/// <summary>
/// SaveManager - שמירת וטעינת משחק
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private string savePath = "/SaveData/";
    [SerializeField] private string saveFileName = "save_game.json";

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
        CreateSaveDirectory();
        Debug.Log("[SaveManager] Initialized");
    }

    /// <summary>
    /// יצירת תיקיית שמירה
    /// </summary>
    private void CreateSaveDirectory()
    {
        string fullPath = Application.persistentDataPath + savePath;
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
            Debug.Log($"[SaveManager] Created save directory: {fullPath}");
        }
    }

    /// <summary>
    /// שמירת משחק
    /// </summary>
    public void SaveGame()
    {
        try
        {
            GameData data = GatherGameData();
            string json = JsonUtility.ToJson(data, true);
            
            string fullPath = Application.persistentDataPath + savePath + saveFileName;
            File.WriteAllText(fullPath, json);
            
            Debug.Log($"[SaveManager] Game saved to: {fullPath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Error saving game: {e.Message}");
        }
    }

    /// <summary>
    /// טעינת משחק
    /// </summary>
    public void LoadGame()
    {
        try
        {
            string fullPath = Application.persistentDataPath + savePath + saveFileName;
            
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("[SaveManager] No save file found!");
                return;
            }

            string json = File.ReadAllText(fullPath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            
            ApplyGameData(data);
            Debug.Log("[SaveManager] Game loaded successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Error loading game: {e.Message}");
        }
    }

    /// <summary>
    /// בדיקה האם יש שמירה
    /// </summary>
    public bool HasSave()
    {
        string fullPath = Application.persistentDataPath + savePath + saveFileName;
        return File.Exists(fullPath);
    }

    /// <summary>
    /// מחיקת שמירה
    /// </summary>
    public void DeleteSave()
    {
        try
        {
            string fullPath = Application.persistentDataPath + savePath + saveFileName;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log("[SaveManager] Save file deleted");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Error deleting save: {e.Message}");
        }
    }

    private GameData GatherGameData()
    {
        PlayerCharacter player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
        
        GameData data = new GameData
        {
            playerName = player != null ? player.characterName : "Unknown",
            playerLevel = player != null ? player.GetLevel() : 1,
            playerHealth = player != null ? player.GetHealth() : 100,
            playerGold = player != null ? player.GetGold() : 0,
            currentScene = SceneController.Instance?.GetCurrentScene() ?? "Unknown",
            playTime = Time.time
        };

        return data;
    }

    private void ApplyGameData(GameData data)
    {
        Debug.Log($"[SaveManager] Loading: {data.playerName} - Level {data.playerLevel}");
        Debug.Log($"[SaveManager] Gold: {data.playerGold}, Health: {data.playerHealth}");
        Debug.Log($"[SaveManager] Play time: {data.playTime}s");
        // Apply loaded data to game state
        var player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.SetLevel(data.playerLevel);
            player.SetHealth(data.playerHealth);

            int currentGold = player.GetGold();
            int delta = data.playerGold - currentGold;
            if (delta > 0) player.AddGold(delta);

            Debug.Log("[SaveManager] Applied save data to player character");
        } else {
            Debug.LogWarning("[SaveManager] No PlayerCharacter found to apply save data");
        }
    }

    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public int playerLevel;
        public int playerHealth;
        public int playerGold;
        public string currentScene;
        public float playTime;
    }
}
