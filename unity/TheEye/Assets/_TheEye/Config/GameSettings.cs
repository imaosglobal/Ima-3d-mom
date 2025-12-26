using UnityEngine;

/// <summary>
/// GameSettings - הגדרות משחק
/// </summary>
public class GameSettings : ScriptableObject
{
    [SerializeField] public float masterVolume = 1f;
    [SerializeField] public float musicVolume = 0.6f;
    [SerializeField] public float sfxVolume = 1f;
    
    [SerializeField] public bool enableVibration = true;
    [SerializeField] public bool enableScreenShake = true;
    [SerializeField] public bool enableBloodEffects = true;
    
    [SerializeField] public int maxFrameRate = 60;
    [SerializeField] public bool useVSync = true;
    
    [SerializeField] public string language = "he"; // he, en
    [SerializeField] public int difficultyLevel = 1; // 1-Easy, 2-Normal, 3-Hard

    public void ApplySettings()
    {
        Time.timeScale = 1f;
        Application.targetFrameRate = maxFrameRate;
        QualitySettings.vSyncCount = useVSync ? 1 : 0;
        Debug.Log("[GameSettings] Settings applied");
    }

    public void SaveSettings()
    {
        // TODO: Implement save to PlayerPrefs
        Debug.Log("[GameSettings] Settings saved");
    }

    public void LoadSettings()
    {
        // TODO: Implement load from PlayerPrefs
        Debug.Log("[GameSettings] Settings loaded");
    }
}
