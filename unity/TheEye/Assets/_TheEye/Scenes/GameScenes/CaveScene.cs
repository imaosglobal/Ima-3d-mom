using UnityEngine;

/// <summary>
/// CaveScene - סצנה מערה (הרפתקה קשה)
/// </summary>
public class CaveScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "Cave";
    [SerializeField] private bool isLit = false;
    [SerializeField] private int bossEnemies = 1;
    [SerializeField] private int minorEnemies = 5;

    private void Start()
    {
        Debug.Log($"[{sceneName}] Entering the Dark Cave!");
        Debug.Log($"[{sceneName}] Lighting: {(isLit ? "Torches" : "DARK")}, Boss Enemies: {bossEnemies}, Minor: {minorEnemies}");
        InitializeCave();
    }

    private void InitializeCave()
    {
        // יציאה של אויבים חזקים - ממלחמות לילה, אדומי אדם
        Debug.Log("[Cave] Dark creatures lurk in the shadows!");
        Debug.Log("[Cave] You can see treasure chests and suspicious glowing objects");
        // עצמים: גוגן, חרוזים זוהרים, חזקים
    }

    public void UseLight(bool torchActive)
    {
        isLit = torchActive;
        Debug.Log($"[{sceneName}] Torch: {(isLit ? "BRIGHT" : "DARK")}");
    }

    public void EncounterBoss()
    {
        Debug.Log("[Cave] A BOSS EMERGES FROM THE DARKNESS!");
        Debug.Log("[Cave] WARNING: Extreme difficulty encounter!");
    }
}
