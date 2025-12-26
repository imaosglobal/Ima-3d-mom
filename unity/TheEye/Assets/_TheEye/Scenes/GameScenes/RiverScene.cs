using UnityEngine;

/// <summary>
/// RiverScene - סצנה נהר (מיוחד - חידוש הנף)
/// </summary>
public class RiverScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "River";
    [SerializeField] private float waterLevel = 2f;
    [SerializeField] private bool canFish = true;
    [SerializeField] private int fishEnemies = 2;

    private void Start()
    {
        Debug.Log($"[{sceneName}] Arrived at the Riverside!");
        Debug.Log($"[{sceneName}] Water Level: {waterLevel}m, Can Fish: {canFish}, Water Enemies: {fishEnemies}");
        InitializeRiver();
    }

    private void InitializeRiver()
    {
        // יציאה של אויבים - צרפדים, זעיר ימיים
        Debug.Log("[River] Water creatures and aquatic monsters live here");
        // עצמים אינטראקטיביים: דגים, צמחי מים, תחנת רחצה
        Debug.Log("[River] You can fish here for rare items!");
    }

    public void FishAtRiver()
    {
        if (canFish)
        {
            Debug.Log("[River] Fishing... Cast your line!");
            // לוגיקת דיג
        }
    }

    public void WadeThrough()
    {
        if (waterLevel > 3f)
        {
            Debug.Log("[River] Water level too high! Cannot cross!");
        }
        else
        {
            Debug.Log("[River] Wading through the river...");
        }
    }

    public void SwimAcross()
    {
        Debug.Log("[River] Swimming across... Be careful of aquatic enemies!");
        // לוגיקת שחייה בסכנה
    }
}
