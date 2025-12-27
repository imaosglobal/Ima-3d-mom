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
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetInt("enableVibration", enableVibration ? 1 : 0);
        PlayerPrefs.SetInt("enableScreenShake", enableScreenShake ? 1 : 0);
        PlayerPrefs.SetInt("enableBloodEffects", enableBloodEffects ? 1 : 0);
        PlayerPrefs.SetInt("maxFrameRate", maxFrameRate);
        PlayerPrefs.SetInt("useVSync", useVSync ? 1 : 0);
        PlayerPrefs.SetString("language", language);
        PlayerPrefs.SetInt("difficultyLevel", difficultyLevel);
        PlayerPrefs.Save();
        Debug.Log("[GameSettings] Settings saved");
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("masterVolume")) masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", sfxVolume);
        enableVibration = PlayerPrefs.GetInt("enableVibration", enableVibration ? 1 : 0) == 1;
        enableScreenShake = PlayerPrefs.GetInt("enableScreenShake", enableScreenShake ? 1 : 0) == 1;
        enableBloodEffects = PlayerPrefs.GetInt("enableBloodEffects", enableBloodEffects ? 1 : 0) == 1;
        maxFrameRate = PlayerPrefs.GetInt("maxFrameRate", maxFrameRate);
        useVSync = PlayerPrefs.GetInt("useVSync", useVSync ? 1 : 0) == 1;
        language = PlayerPrefs.GetString("language", language);
        difficultyLevel = PlayerPrefs.GetInt("difficultyLevel", difficultyLevel);
        Debug.Log("[GameSettings] Settings loaded");
    }
}
