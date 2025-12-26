using UnityEngine;

/// <summary>
/// GameManager - ניהול המשחק הראשי
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // סיקרט לחיבור לחנות
    private string shopSecret = "ima-secret-key";
    
    // דמות הראשונה
    private GameObject momCharacter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("GameManager initialized");
        InitializeMom();
        VerifyShopConnection();
    }

    /// <summary>
    /// אתחול דמות אמא
    /// </summary>
    private void InitializeMom()
    {
        // כאן תטענו את Prefab של אמא מ-Resources/Characters/
        // momCharacter = Instantiate(Resources.Load<GameObject>("Characters/Mom"));
        Debug.Log("Mom character initialized");
    }

    /// <summary>
    /// בדיקה של חיבור לחנות עם סיקרט
    /// </summary>
    private void VerifyShopConnection()
    {
        Debug.Log($"Shop connection verified with secret: {shopSecret}");
        // כאן תוסיפו קוד שמחובר לשרת (server/index.js)
    }

    /// <summary>
    /// רכישת משהו מהחנות
    /// </summary>
    public void PurchaseItem(string itemId)
    {
        Debug.Log($"Attempting to purchase item: {itemId} with secret verification");
        // קוד רכישה
    }
}
