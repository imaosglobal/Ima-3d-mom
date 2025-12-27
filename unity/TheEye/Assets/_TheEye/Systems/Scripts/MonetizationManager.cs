using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// MonetizationManager - מערכת מונטיזציה (קניות + פרסומות)
/// </summary>
public class MonetizationManager : MonoBehaviour
{
    public static MonetizationManager Instance { get; private set; }

    [SerializeField] private List<IAP_Product> iapProducts = new List<IAP_Product>();
    [SerializeField] private bool adsEnabled = true;
    [SerializeField] private bool premiumMode = false;
    [SerializeField] private float adFrequencyMinutes = 5f;

    private float lastAdTime = 0f;
    private int totalRevenue = 0;
    private Dictionary<string, bool> purchasedItems = new Dictionary<string, bool>();

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
        InitializeProducts();
        Debug.Log("[Monetization] System initialized");
    }

    private void InitializeProducts()
    {
        // יציאת מוצרי IAP לדוגמה
        iapProducts.Add(new IAP_Product
        {
            productId = "ima_gold_100",
            productName = "100 Gold",
            price = 0.99f,
            description = "Buy 100 gold to upgrade your items",
            currencyCode = "USD"
        });

        iapProducts.Add(new IAP_Product
        {
            productId = "ima_gold_500",
            productName = "500 Gold Bundle",
            price = 4.99f,
            description = "Get 500 gold at a discount",
            currencyCode = "USD"
        });

        iapProducts.Add(new IAP_Product
        {
            productId = "ima_premium_pass",
            productName = "Premium Pass",
            price = 9.99f,
            description = "Remove ads & unlock exclusive features",
            currencyCode = "USD"
        });

        Debug.Log($"[Monetization] Loaded {iapProducts.Count} products");
    }

    /// <summary>
    /// קנייה של מוצר
    /// </summary>
    public void PurchaseProduct(string productId)
    {
        IAP_Product product = iapProducts.Find(p => p.productId == productId);
        if (product == null)
        {
            Debug.LogError($"[Monetization] Product not found: {productId}");
            return;
        }

        Debug.Log($"[Monetization] Purchasing {product.productName} for ${product.price}");
        
        // Integration point: Replace with platform IAP SDK (Unity IAP / Play Billing / App Store) when ready
        // For now, simulate purchase
        SimulatePurchase(productId, product);
    }

    private void SimulatePurchase(string productId, IAP_Product product)
    {
        purchasedItems[productId] = true;
        totalRevenue += (int)(product.price * 100); // cents
        
        Debug.Log($"[Monetization] ✅ Purchase successful: {product.productName}");
        Debug.Log($"[Monetization] Total revenue: ${totalRevenue / 100f}");

        if (productId == "ima_premium_pass")
        {
            premiumMode = true;
            adsEnabled = false;
            Debug.Log("[Monetization] Premium mode activated - ads disabled");
        }
    }

    /// <summary>
    /// הצג פרסומת
    /// </summary>
    public void ShowAd()
    {
        if (premiumMode || !adsEnabled)
        {
            Debug.Log("[Monetization] Ads disabled");
            return;
        }

        if (Time.time - lastAdTime < adFrequencyMinutes * 60)
        {
            Debug.Log("[Monetization] Ad frequency limit not reached");
            return;
        }

        Debug.Log("[Monetization] 📺 Showing advertisement (simulated)");
        lastAdTime = Time.time;
        // Simulate ad callback
        OnAdFinished();
    }

    /// <summary>
    /// בדוק אם מוצר נקנה
    /// </summary>
    public bool IsPurchased(string productId)
    {
        return purchasedItems.ContainsKey(productId) && purchasedItems[productId];
    }

    /// <summary>
    /// קבל סטטוס פרימיום
    /// </summary>
    public bool IsPremium()
    {
        return premiumMode;
    }

    public int GetTotalRevenue() => totalRevenue;
    public int GetProductCount() => iapProducts.Count;
}

[System.Serializable]
public class IAP_Product
{
    public string productId;
    public string productName;
    public float price;
    public string description;
    public string currencyCode = "USD";
    public Sprite productIcon;
}
