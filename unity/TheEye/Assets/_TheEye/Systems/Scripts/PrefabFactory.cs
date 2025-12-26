using UnityEngine;

/// <summary>
/// PrefabFactory - מפעל יצירת Prefabs דינמיים
/// </summary>
public class PrefabFactory : MonoBehaviour
{
    /// <summary>
    /// יצירת דמות שחקן מסוג מסוים
    /// </summary>
    public static GameObject CreatePlayer(CharacterRole role, Vector3 position)
    {
        GameObject playerObj = new GameObject($"Player_{role}");
        playerObj.transform.position = position;
        playerObj.tag = "Player";

        // הוסף Collider ו-Rigidbody
        var collider = playerObj.AddComponent<CapsuleCollider>();
        collider.center = Vector3.zero;
        var rb = playerObj.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // הוסף את הדמות המתאימה
        PlayerCharacter player = null;
        switch (role)
        {
            case CharacterRole.Creator:
                player = playerObj.AddComponent<CreatorCharacter>();
                break;
            case CharacterRole.Hunter:
                player = playerObj.AddComponent<HunterCharacter>();
                break;
            case CharacterRole.Mage:
                player = playerObj.AddComponent<MageCharacter>();
                break;
            case CharacterRole.Healer:
                player = playerObj.AddComponent<HealerCharacter>();
                break;
            case CharacterRole.Explorer:
                player = playerObj.AddComponent<ExplorerCharacter>();
                break;
        }

        // הוסף מערכות נדרשות
        playerObj.AddComponent<Inventory>();
        playerObj.AddComponent<QuestManager>();
        playerObj.AddComponent<AnimationManager>();

        Debug.Log($"[PrefabFactory] Created Player: {role}");
        return playerObj;
    }

    /// <summary>
    /// יצירת דמות אמא (Ima)
    /// </summary>
    public static GameObject CreateImaCharacter(Vector3 position)
    {
        GameObject imaObj = new GameObject("Ima_Mother");
        imaObj.transform.position = position;
        imaObj.tag = "NPC";

        // הוסף Collider ו-Rigidbody
        var collider = imaObj.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        var rb = imaObj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // הוסף את דמות אמא
        var ima = imaObj.AddComponent<ImaCharacter>();
        imaObj.AddComponent<AnimationManager>();

        Debug.Log("[PrefabFactory] Created Ima Character");
        return imaObj;
    }

    /// <summary>
    /// יצירת NPC
    /// </summary>
    public static GameObject CreateNPC(string npcType, string name, Vector3 position)
    {
        GameObject npcObj = new GameObject($"NPC_{name}");
        npcObj.transform.position = position;
        npcObj.tag = "NPC";

        // הוסף Collider ו-Rigidbody
        var collider = npcObj.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        var rb = npcObj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // הוסף את סוג ה-NPC המתאים
        NPCCharacter npc = null;
        switch (npcType.ToLower())
        {
            case "shopkeeper":
                npc = npcObj.AddComponent<Shopkeeper>();
                break;
            case "questgiver":
                npc = npcObj.AddComponent<QuestGiver>();
                break;
            case "doctor":
                npc = npcObj.AddComponent<Doctor>();
                break;
            case "blacksmith":
                npc = npcObj.AddComponent<Blacksmith>();
                break;
            default:
                npc = npcObj.AddComponent<NPCCharacter>();
                break;
        }

        npc.characterName = name;
        npcObj.AddComponent<AnimationManager>();

        Debug.Log($"[PrefabFactory] Created NPC: {npcType} - {name}");
        return npcObj;
    }

    /// <summary>
    /// יצירת אויב
    /// </summary>
    public static GameObject CreateEnemy(string enemyType, int difficulty, Vector3 position)
    {
        GameObject enemyObj = new GameObject($"Enemy_{enemyType}_{difficulty}");
        enemyObj.transform.position = position;
        enemyObj.tag = "Enemy";

        // הוסף Collider ו-Rigidbody
        var collider = enemyObj.AddComponent<CapsuleCollider>();
        var rb = enemyObj.AddComponent<Rigidbody>();

        // הוסף את סוג האויב המתאים
        EnemyCharacter enemy = null;
        switch (enemyType.ToLower())
        {
            case "wolf":
                enemy = enemyObj.AddComponent<WildEnemy>();
                break;
            case "ogre":
                enemy = enemyObj.AddComponent<CaveMonster>();
                break;
            case "naga":
                enemy = enemyObj.AddComponent<WaterCreature>();
                break;
            case "boss":
                enemy = enemyObj.AddComponent<BossEnemy>();
                break;
            default:
                enemy = enemyObj.AddComponent<EnemyCharacter>();
                break;
        }

        enemy.difficulty = difficulty;
        enemyObj.AddComponent<AnimationManager>();

        Debug.Log($"[PrefabFactory] Created Enemy: {enemyType} (Difficulty: {difficulty})");
        return enemyObj;
    }
}
