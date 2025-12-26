using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// DistributionManager - ניהול ערוצי הפצה
/// </summary>
public class DistributionManager : MonoBehaviour
{
    public static DistributionManager Instance { get; private set; }

    [SerializeField] private List<DistributionChannel> channels = new List<DistributionChannel>();
    [SerializeField] private string gameTitle = "The Eye RPG";
    [SerializeField] private string gameId = "com.imaosglobal.theeye";
    [SerializeField] private string version = "1.0.0";

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
        InitializeChannels();
        Debug.Log("[Distribution] System initialized");
    }

    private void InitializeChannels()
    {
        channels.Add(new DistributionChannel
        {
            name = "GitHub Pages",
            url = "https://ima-eye-rpg.games",
            type = ChannelType.Web,
            status = ChannelStatus.Active
        });

        channels.Add(new DistributionChannel
        {
            name = "Google Play Store",
            url = "https://play.google.com/store/apps/details?id=" + gameId,
            type = ChannelType.Android,
            status = ChannelStatus.Active
        });

        channels.Add(new DistributionChannel
        {
            name = "Apple App Store",
            url = "https://apps.apple.com/app/the-eye-rpg",
            type = ChannelType.iOS,
            status = ChannelStatus.Active
        });

        channels.Add(new DistributionChannel
        {
            name = "Steam",
            url = "https://store.steampowered.com/app/the-eye-rpg",
            type = ChannelType.PC,
            status = ChannelStatus.InReview
        });

        channels.Add(new DistributionChannel
        {
            name = "itch.io",
            url = "https://imaosglobal.itch.io/the-eye-rpg",
            type = ChannelType.Web,
            status = ChannelStatus.Active
        });

        Debug.Log($"[Distribution] Loaded {channels.Count} distribution channels");
    }

    /// <summary>
    /// קבל מידע ערוץ
    /// </summary>
    public DistributionChannel GetChannel(string name)
    {
        return channels.Find(c => c.name == name);
    }

    /// <summary>
    /// קבל כל הערוצים הפעילים
    /// </summary>
    public List<DistributionChannel> GetActiveChannels()
    {
        return channels.FindAll(c => c.status == ChannelStatus.Active);
    }

    /// <summary>
    /// פתח ערוץ הפצה
    /// </summary>
    public void OpenChannel(string channelName)
    {
        var channel = GetChannel(channelName);
        if (channel == null)
        {
            Debug.LogWarning($"[Distribution] Channel not found: {channelName}");
            return;
        }

        Debug.Log($"[Distribution] Opening {channelName}: {channel.url}");
        Application.OpenURL(channel.url);
    }

    /// <summary>
    /// דוח הפצה
    /// </summary>
    public void PrintDistributionReport()
    {
        Debug.Log($"\n{'='.ToString().PadRight(50, '=')}");
        Debug.Log($"📦 DISTRIBUTION REPORT - {gameTitle} v{version}");
        Debug.Log($"{'='.ToString().PadRight(50, '=')}");

        foreach (var channel in channels)
        {
            var statusEmoji = channel.status == ChannelStatus.Active ? "✅" : "⏳";
            Debug.Log($"{statusEmoji} {channel.name} ({channel.type})");
            Debug.Log($"   URL: {channel.url}");
            Debug.Log($"   Status: {channel.status}");
        }

        Debug.Log($"{'='.ToString().PadRight(50, '=')}");
        Debug.Log($"Total Channels: {channels.Count}");
        Debug.Log($"Active Channels: {GetActiveChannels().Count}");
    }

    public int GetChannelCount() => channels.Count;
    public int GetActiveChannelCount() => GetActiveChannels().Count;
}

[System.Serializable]
public class DistributionChannel
{
    public string name;
    public string url;
    public ChannelType type;
    public ChannelStatus status;
    public int downloads = 0;
    public float rating = 0f;
    public int reviews = 0;
}

public enum ChannelType
{
    Web,
    Android,
    iOS,
    PC,
    Console
}

public enum ChannelStatus
{
    Active,
    InReview,
    Pending,
    Inactive
}
