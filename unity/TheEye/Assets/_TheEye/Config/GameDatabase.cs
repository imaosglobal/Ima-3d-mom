using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameDatabase - מסד נתונים של המשחק
/// </summary>
public class GameDatabase : ScriptableObject
{
    [SerializeField] public List<CharacterTemplate> characters = new List<CharacterTemplate>();
    [SerializeField] public List<EnemyTemplate> enemies = new List<EnemyTemplate>();
    [SerializeField] public List<ItemTemplate> items = new List<ItemTemplate>();
    [SerializeField] public List<QuestTemplate> quests = new List<QuestTemplate>();

    public CharacterTemplate GetCharacterTemplate(string name)
    {
        return characters.Find(c => c.characterName == name);
    }

    public EnemyTemplate GetEnemyTemplate(string name)
    {
        return enemies.Find(e => e.enemyName == name);
    }

    public ItemTemplate GetItemTemplate(string name)
    {
        return items.Find(i => i.itemName == name);
    }

    public QuestTemplate GetQuestTemplate(string name)
    {
        return quests.Find(q => q.questName == name);
    }
}

[System.Serializable]
public class CharacterTemplate
{
    public string characterName;
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public Sprite portrait;
}

[System.Serializable]
public class EnemyTemplate
{
    public string enemyName;
    public int baseHealth = 30;
    public int difficulty = 1;
    public float speed = 3f;
    public int goldReward = 10;
    public int expReward = 50;
    public Sprite enemySprite;
}

[System.Serializable]
public class ItemTemplate
{
    public string itemName;
    public ItemType type;
    public int value = 0;
    public string description;
    public Sprite icon;
}

[System.Serializable]
public class QuestTemplate
{
    public string questName;
    public string description;
    public int rewardXP = 100;
    public int rewardGold = 50;
    public int targetCount = 1;
    public string giver;
}
