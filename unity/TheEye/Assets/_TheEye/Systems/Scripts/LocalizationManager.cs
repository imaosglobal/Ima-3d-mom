using UnityEngine;

/// <summary>
/// LocalizationManager - ניהול שפות במשחק
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    [SerializeField] private string currentLanguage = "he"; // he = עברית, en = אנגלית
    
    private System.Collections.Generic.Dictionary<string, string> hebrewStrings;
    private System.Collections.Generic.Dictionary<string, string> englishStrings;
    private System.Collections.Generic.Dictionary<string, string> currentStrings;

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
        InitializeLanguages();
        SetLanguage(currentLanguage);
        Debug.Log($"[LocalizationManager] Language set to: {currentLanguage}");
    }

    private void InitializeLanguages()
    {
        hebrewStrings = new System.Collections.Generic.Dictionary<string, string>
        {
            { "welcome", "ברוכים הבאים ל-The Eye!" },
            { "new_game", "משחק חדש" },
            { "continue", "המשך" },
            { "settings", "הגדרות" },
            { "quit", "צא" },
            { "health", "בריאות" },
            { "level", "רמה" },
            { "gold", "זהב" },
            { "experience", "ניסיון" },
            { "inventory", "תיק" },
            { "quests", "משימות" },
        };

        englishStrings = new System.Collections.Generic.Dictionary<string, string>
        {
            { "welcome", "Welcome to The Eye!" },
            { "new_game", "New Game" },
            { "continue", "Continue" },
            { "settings", "Settings" },
            { "quit", "Quit" },
            { "health", "Health" },
            { "level", "Level" },
            { "gold", "Gold" },
            { "experience", "Experience" },
            { "inventory", "Inventory" },
            { "quests", "Quests" },
        };
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;
        currentStrings = language == "he" ? hebrewStrings : englishStrings;
        Debug.Log($"[LocalizationManager] Language changed to: {language}");
    }

    public string GetString(string key)
    {
        if (currentStrings != null && currentStrings.ContainsKey(key))
        {
            return currentStrings[key];
        }
        Debug.LogWarning($"[LocalizationManager] String key not found: {key}");
        return key;
    }

    public string GetCurrentLanguage() => currentLanguage;
}
