using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// AnalyticsManager - מערכת ניתוח משחקנים ופעילות
/// </summary>
public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    [SerializeField] private string gameVersion = "1.0.0";
    [SerializeField] private string analyticsUrl = "https://analytics.ima-eye-rpg.games/track";

    private Dictionary<string, int> eventCounters = new Dictionary<string, int>();
    private float sessionStartTime = 0f;
    private int totalSessionTime = 0;
    private int gamesPlayed = 0;
    private float averageSessionLength = 0f;

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
        sessionStartTime = Time.time;
        Debug.Log("[Analytics] Session started");
        TrackEvent("game_session_start");
    }

    /// <summary>
    /// רישום אירוע
    /// </summary>
    public void TrackEvent(string eventName)
    {
        if (!eventCounters.ContainsKey(eventName))
        {
            eventCounters[eventName] = 0;
        }
        eventCounters[eventName]++;

        Debug.Log($"[Analytics] Event tracked: {eventName}");
        SendAnalytics(eventName);
    }

    /// <summary>
    /// רישום קנייה
    /// </summary>
    public void TrackPurchase(string productId, float price, string currency)
    {
        var purchaseData = new
        {
            productId = productId,
            price = price,
            currency = currency,
            timestamp = System.DateTime.Now
        };

        Debug.Log($"[Analytics] Purchase tracked: {productId} - ${price} {currency}");
        TrackEvent("purchase_" + productId);
    }

    /// <summary>
    /// רישום ניצחון
    /// </summary>
    public void TrackVictory(CharacterBase winner, int score)
    {
        Debug.Log($"[Analytics] Victory tracked: {winner.characterName} - Score: {score}");
        TrackEvent("player_victory");
    }

    /// <summary>
    /// רישום הפסד
    /// </summary>
    public void TrackDefeat(CharacterBase loser)
    {
        Debug.Log($"[Analytics] Defeat tracked: {loser.characterName}");
        TrackEvent("player_defeat");
    }

    /// <summary>
    /// שלח נתונים לשרת
    /// </summary>
    private void SendAnalytics(string eventName)
    {
        // TODO: Send to Firebase/Google Analytics
        // For now, just log
        var analyticsData = new
        {
            eventName = eventName,
            timestamp = System.DateTime.UtcNow,
            sessionId = Random.Range(100000, 999999),
            deviceId = SystemInfo.deviceUniqueIdentifier,
            platform = Application.platform.ToString(),
            gameVersion = gameVersion
        };

        Debug.Log("[Analytics] Data sent: " + eventName);
    }

    /// <summary>
    /// קבל סטטיסטיקות שרתים
    /// </summary>
    public Dictionary<string, int> GetEventCounters() => eventCounters;

    public int GetGamesPlayed() => gamesPlayed;
    public float GetAverageSessionLength() => averageSessionLength;

    private void OnApplicationQuit()
    {
        totalSessionTime = (int)(Time.time - sessionStartTime);
        Debug.Log($"[Analytics] Session ended - Duration: {totalSessionTime}s");
        TrackEvent("game_session_end");
    }
}

/// <summary>
/// MarketingManager - קמפיינים שיווק
/// </summary>
public class MarketingManager : MonoBehaviour
{
    public static MarketingManager Instance { get; private set; }

    [SerializeField] private List<Campaign> activeCampaigns = new List<Campaign>();
    [SerializeField] private string appStoreUrl = "https://apps.apple.com/app/the-eye-rpg";
    [SerializeField] private string playStoreUrl = "https://play.google.com/store/apps/details?id=com.imaosglobal.theeye";
    [SerializeField] private string webUrl = "https://ima-eye-rpg.games";

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
        InitializeCampaigns();
        Debug.Log("[Marketing] System initialized");
    }

    private void InitializeCampaigns()
    {
        activeCampaigns.Add(new Campaign
        {
            campaignId = "launch_2025",
            name = "Launch Campaign",
            description = "Celebrate The Eye RPG release",
            discount = 50,
            startDate = System.DateTime.Now,
            endDate = System.DateTime.Now.AddDays(30)
        });

        activeCampaigns.Add(new Campaign
        {
            campaignId = "holiday_special",
            name = "Holiday Special",
            description = "Special holiday offers",
            discount = 30,
            startDate = System.DateTime.Now,
            endDate = System.DateTime.Now.AddDays(14)
        });

        Debug.Log($"[Marketing] Loaded {activeCampaigns.Count} campaigns");
    }

    /// <summary>
    /// קבל קמפיינים פעילים
    /// </summary>
    public List<Campaign> GetActiveCampaigns()
    {
        var active = new List<Campaign>();
        foreach (var campaign in activeCampaigns)
        {
            if (System.DateTime.Now >= campaign.startDate && System.DateTime.Now <= campaign.endDate)
            {
                active.Add(campaign);
            }
        }
        return active;
    }

    /// <summary>
    /// פתח בחנות אפליקציות
    /// </summary>
    public void OpenAppStore()
    {
        Debug.Log("[Marketing] Opening App Store: " + appStoreUrl);
        Application.OpenURL(appStoreUrl);
    }

    public void OpenPlayStore()
    {
        Debug.Log("[Marketing] Opening Play Store: " + playStoreUrl);
        Application.OpenURL(playStoreUrl);
    }

    public void OpenWebsite()
    {
        Debug.Log("[Marketing] Opening website: " + webUrl);
        Application.OpenURL(webUrl);
    }

    /// <summary>
    /// שתף משחק ברשתות חברתיות
    /// </summary>
    public void ShareOnSocial(string platform, string message)
    {
        Debug.Log($"[Marketing] Sharing on {platform}: {message}");
        
        if (platform == "twitter")
        {
            Application.OpenURL($"https://twitter.com/intent/tweet?text={message}");
        }
        else if (platform == "facebook")
        {
            Application.OpenURL($"https://www.facebook.com/sharer/sharer.php?u={webUrl}");
        }
    }
}

[System.Serializable]
public class Campaign
{
    public string campaignId;
    public string name;
    public string description;
    public int discount; // percentage
    public System.DateTime startDate;
    public System.DateTime endDate;
    public Sprite campaignImage;
}
