using UnityEngine;

/// <summary>
/// ForestScene - סצנה יער (הרפתקה קלה)
/// </summary>
public class ForestScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "Forest";
    [SerializeField] private bool hasRain = false;
    [SerializeField] private int enemyCount = 3;

    private void Start()
    {
        Debug.Log($"[{sceneName}] Entering the Dense Forest!");
        Debug.Log($"[{sceneName}] Rain: {(hasRain ? "Yes" : "No")}, Enemies: {enemyCount}");
        InitializeForest();
    }

    private void InitializeForest()
    {
        // יציאה של אויבים - חיות יער, גוברלינים
        Debug.Log("[Forest] Wolves and Goblins patrol the area!");
        // עצמים אינטראקטיביים: עצים, יתושים, פירות
        Debug.Log("[Forest] You see fruit trees, mushrooms, and hidden treasures");
    }

    public void OnExplore()
    {
        Debug.Log("[Forest] Exploring... You discover a hidden path!");
        // לוגיקת ממצאים
    }

    public void OnWeatherChange()
    {
        hasRain = !hasRain;
        Debug.Log($"[{sceneName}] Weather changed - Rain: {hasRain}");
    }
}
