using UnityEngine;

/// <summary>
/// VillageScene - סצנה הכפר (בית)
/// </summary>
public class VillageScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "Village";
    [SerializeField] private int timeOfDay = 12; // 0-23
    [SerializeField] private float temperatureCelsius = 20f;

    private void Start()
    {
        Debug.Log($"[{sceneName}] Welcome to the Village!");
        Debug.Log($"[{sceneName}] Time: {timeOfDay}:00, Temperature: {temperatureCelsius}°C");
        Debug.Log("[Village] Home sweet home. Ima is here.");
        InitializeVillage();
    }

    private void InitializeVillage()
    {
        // יציאה של NPCs - חנוני כפר, רופא, וגו'
        Debug.Log("[Village] Shopkeeper, Doctor, and other villagers are present");
        // ניתן להוסיף עצמים אינטראקטיביים: חנות, בית רפואה, תאום
    }

    public void UpdateTimeOfDay(int newTime)
    {
        timeOfDay = newTime;
        Debug.Log($"[{sceneName}] Time changed to {timeOfDay}:00");
        UpdateLighting();
    }

    private void UpdateLighting()
    {
        // הסתרה של סצנה לפי שעה - יום/לילה
        if (timeOfDay >= 6 && timeOfDay < 18)
        {
            Debug.Log("[Village] Day - Bright lighting");
        }
        else
        {
            Debug.Log("[Village] Night - Dark lighting, NPCs sleeping");
        }
    }

    public void OnInteractWithNPC(string npcName)
    {
        Debug.Log($"[Village] Interacting with {npcName}");
    }
}
