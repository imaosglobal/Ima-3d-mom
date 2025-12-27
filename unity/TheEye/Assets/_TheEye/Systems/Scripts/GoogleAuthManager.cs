using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GoogleAuthManager - אימות דרך חשבון Google
/// </summary>
public class GoogleAuthManager : MonoBehaviour
{
    public static GoogleAuthManager Instance { get; private set; }

    [SerializeField] private string googleClientId = "YOUR_GOOGLE_CLIENT_ID";
    [SerializeField] private bool isAuthenticated = false;
    private string currentUserId = "";
    private string userEmail = "";
    private string userName = "";

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
        Debug.Log("[GoogleAuth] Initializing Google Sign-In (simulated)");
        // Simulate SDK initialization for local testing
    }

    /// <summary>
    /// התחברות דרך Google
    /// </summary>
    public void SignInWithGoogle()
    {
        Debug.Log("[GoogleAuth] Starting sign-in flow...");
        
        // Simulated Google Sign-In (placeholder for real SDK integration)
        // Integration points: Play Services SDK (Android) or Firebase Auth (cross-platform)
        SimulateGoogleSignIn();
    }

    private void SimulateGoogleSignIn()
    {
        isAuthenticated = true;
        currentUserId = "user_" + Random.Range(100000, 999999);
        userEmail = "player@imaosglobal.com";
        userName = "Player_" + Random.Range(1000, 9999);
        
        Debug.Log($"[GoogleAuth] ✅ Signed in as: {userName} ({userEmail})");
        Debug.Log($"[GoogleAuth] User ID: {currentUserId}");
    }

    /// <summary>
    /// התנתקות
    /// </summary>
    public void SignOut()
    {
        isAuthenticated = false;
        currentUserId = "";
        userEmail = "";
        userName = "";
        
        Debug.Log("[GoogleAuth] Signed out");
    }

    public bool IsAuthenticated() => isAuthenticated;
    public string GetUserId() => currentUserId;
    public string GetUserEmail() => userEmail;
    public string GetUserName() => userName;
}

/// <summary>
/// GeoLocalizationManager - התאמה מקומית לפי מיקום
/// </summary>
public class GeoLocalizationManager : MonoBehaviour
{
    public static GeoLocalizationManager Instance { get; private set; }

    [SerializeField] private string currentCountry = "US";
    [SerializeField] private string currentLanguage = "en";
    [SerializeField] private string currentCurrency = "USD";
    [SerializeField] private Dictionary<string, RegionSettings> regionDatabase = new Dictionary<string, RegionSettings>();

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
        InitializeRegions();
        DetectUserLocation();
        Debug.Log("[GeoLocalization] Initialized");
    }

    private void InitializeRegions()
    {
        // Israel
        regionDatabase["IL"] = new RegionSettings
        {
            countryCode = "IL",
            countryName = "Israel",
            language = "he",
            currency = "ILS",
            currencySymbol = "₪",
            timezone = "Asia/Jerusalem"
        };

        // USA
        regionDatabase["US"] = new RegionSettings
        {
            countryCode = "US",
            countryName = "United States",
            language = "en",
            currency = "USD",
            currencySymbol = "$",
            timezone = "America/New_York"
        };

        // Europe
        regionDatabase["EU"] = new RegionSettings
        {
            countryCode = "EU",
            countryName = "Europe",
            language = "en",
            currency = "EUR",
            currencySymbol = "€",
            timezone = "Europe/London"
        };

        Debug.Log($"[GeoLocalization] Loaded {regionDatabase.Count} regions");
    }

    /// <summary>
    /// גלה מיקום משתמש
    /// </summary>
    private void DetectUserLocation()
    {
        // Check if user is in Israel by Google Sign-In
        if (GoogleAuthManager.Instance && GoogleAuthManager.Instance.IsAuthenticated())
        {
                // Attempt to infer location from authenticated user (simple heuristic)
            if (GoogleAuthManager.Instance != null && GoogleAuthManager.Instance.IsAuthenticated())
            {
                var email = GoogleAuthManager.Instance.GetUserEmail();
                if (!string.IsNullOrEmpty(email) && email.EndsWith(".il"))
                {
                    if (regionDatabase.ContainsKey("IL")) ApplyRegionSettings("IL");
                    return;
                }
                // default authenticated country: IL for local testing
                if (regionDatabase.ContainsKey("IL"))
                {
                    ApplyRegionSettings("IL");
                    return;
                }
            }

            // Default to US
            if (regionDatabase.ContainsKey("US"))
            {
                ApplyRegionSettings("US");
            }
        }
        else
        {
            // Default to US
            if (regionDatabase.ContainsKey("US"))
            {
                ApplyRegionSettings("US");
            }
        }
    }

    /// <summary>
    /// החל הגדרות אזור
    /// </summary>
    public void ApplyRegionSettings(string countryCode)
    {
        if (!regionDatabase.ContainsKey(countryCode))
        {
            Debug.LogWarning($"[GeoLocalization] Country not found: {countryCode}");
            return;
        }

        var settings = regionDatabase[countryCode];
        currentCountry = countryCode;
        currentLanguage = settings.language;
        currentCurrency = settings.currency;

        Debug.Log($"[GeoLocalization] Applied settings for {settings.countryName}");
        Debug.Log($"[GeoLocalization] Language: {currentLanguage}, Currency: {currentCurrency}");
    }

    public string GetCurrentCountry() => currentCountry;
    public string GetCurrentLanguage() => currentLanguage;
    public string GetCurrentCurrency() => currentCurrency;
    public RegionSettings GetRegionSettings(string countryCode)
    {
        return regionDatabase.ContainsKey(countryCode) ? regionDatabase[countryCode] : null;
    }
}

[System.Serializable]
public class RegionSettings
{
    public string countryCode;
    public string countryName;
    public string language;
    public string currency;
    public string currencySymbol;
    public string timezone;
}
