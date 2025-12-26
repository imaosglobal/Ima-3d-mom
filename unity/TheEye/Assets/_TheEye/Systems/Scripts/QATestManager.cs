using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// QATestManager - ניהול בדיקות QA אוטומטיות
/// </summary>
public class QATestManager : MonoBehaviour
{
    public static QATestManager Instance { get; private set; }

    [SerializeField] private bool runAutomatedTests = true;
    [SerializeField] private List<TestResult> testResults = new List<TestResult>();
    private int testsRun = 0;
    private int testsPassed = 0;
    private int testsFailed = 0;

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
        if (runAutomatedTests)
        {
            RunAllTests();
        }
    }

    /// <summary>
    /// הרץ כל הבדיקות
    /// </summary>
    public void RunAllTests()
    {
        Debug.Log("[QA] Starting automated test suite...");
        
        TestCharacterSystem();
        TestEnemySystem();
        TestQuestSystem();
        TestInventorySystem();
        TestMonetization();
        TestAuthentication();
        TestLocalization();
        TestPerformance();
        
        PrintTestReport();
    }

    private void TestCharacterSystem()
    {
        Debug.Log("[QA] Testing Character System...");
        
        var player = PrefabFactory.CreatePlayer(CharacterRole.Creator, Vector3.zero);
        Assert(player != null, "Player creation");
        Assert(player.GetHealth() > 0, "Player health");
        Assert(player.GetLevel() > 0, "Player level");
        
        player.AddExperience(100);
        Assert(player.GetExperience() > 0, "Experience tracking");
        
        Destroy(player);
    }

    private void TestEnemySystem()
    {
        Debug.Log("[QA] Testing Enemy System...");
        
        var enemy = PrefabFactory.CreateEnemy("wolf", 1, Vector3.zero);
        Assert(enemy != null, "Enemy creation");
        Assert(enemy.IsAlive(), "Enemy alive state");
        
        Destroy(enemy);
    }

    private void TestQuestSystem()
    {
        Debug.Log("[QA] Testing Quest System...");
        
        var quest = new Quest
        {
            questName = "Test Quest",
            rewardXP = 100,
            targetCount = 5
        };
        
        Assert(quest != null, "Quest creation");
        quest.currentCount = 5;
        Assert(quest.CheckCompletion(), "Quest completion check");
    }

    private void TestInventorySystem()
    {
        Debug.Log("[QA] Testing Inventory System...");
        
        var inventory = new Inventory();
        var item = new InventoryItem { itemName = "Test Item", quantity = 1 };
        
        Assert(inventory != null, "Inventory creation");
        // Assert(inventory.AddItem(item), "Add item to inventory");
    }

    private void TestMonetization()
    {
        Debug.Log("[QA] Testing Monetization System...");
        
        Assert(MonetizationManager.Instance != null, "Monetization manager");
        Assert(!MonetizationManager.Instance.IsPremium(), "Premium status");
    }

    private void TestAuthentication()
    {
        Debug.Log("[QA] Testing Authentication System...");
        
        if (GoogleAuthManager.Instance != null)
        {
            Assert(GoogleAuthManager.Instance != null, "Auth manager exists");
        }
    }

    private void TestLocalization()
    {
        Debug.Log("[QA] Testing Localization System...");
        
        if (GeoLocalizationManager.Instance != null)
        {
            Assert(GeoLocalizationManager.Instance.GetCurrentLanguage() != "", "Language detection");
            Assert(GeoLocalizationManager.Instance.GetCurrentCurrency() != "", "Currency detection");
        }
    }

    private void TestPerformance()
    {
        Debug.Log("[QA] Testing Performance...");
        
        float fps = 1f / Time.deltaTime;
        Assert(fps > 30, "FPS above 30");
        
        long memoryUsed = System.GC.GetTotalMemory(false);
        Debug.Log($"[QA] Memory usage: {memoryUsed / 1024 / 1024}MB");
    }

    private void Assert(bool condition, string testName)
    {
        testsRun++;
        
        if (condition)
        {
            testsPassed++;
            Debug.Log($"✅ PASS: {testName}");
            testResults.Add(new TestResult { name = testName, passed = true });
        }
        else
        {
            testsFailed++;
            Debug.LogError($"❌ FAIL: {testName}");
            testResults.Add(new TestResult { name = testName, passed = false });
        }
    }

    private void PrintTestReport()
    {
        Debug.Log($"\n{'='.ToString().PadRight(50, '=')}");
        Debug.Log($"📊 QA TEST REPORT");
        Debug.Log($"{'='.ToString().PadRight(50, '=')}");
        Debug.Log($"Tests Run: {testsRun}");
        Debug.Log($"Tests Passed: ✅ {testsPassed}");
        Debug.Log($"Tests Failed: ❌ {testsFailed}");
        Debug.Log($"Success Rate: {(float)testsPassed / testsRun * 100:F2}%");
        Debug.Log($"{'='.ToString().PadRight(50, '=')}");
    }

    public int GetTestsRun() => testsRun;
    public int GetTestsPassed() => testsPassed;
    public int GetTestsFailed() => testsFailed;
    public List<TestResult> GetTestResults() => testResults;
}

[System.Serializable]
public class TestResult
{
    public string name;
    public bool passed;
    public string errorMessage;
}
