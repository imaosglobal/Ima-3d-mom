using UnityEngine;

/// <summary>
/// WildEnemy - אויב יער בסיסי
/// </summary>
public class WildEnemy : EnemyCharacter
{
    private void Start()
    {
        characterName = "Forest Wolf";
        difficulty = 1;
        maxHealth = 30;
        currentHealth = maxHealth;
        moveSpeed = 4f;
        Debug.Log("[Wolf] Growls menacingly!");
    }
}

/// <summary>
/// CaveMonster - איום מערה
/// </summary>
public class CaveMonster : EnemyCharacter
{
    private void Start()
    {
        characterName = "Cave Ogre";
        difficulty = 3;
        maxHealth = 100;
        currentHealth = maxHealth;
        moveSpeed = 3f;
        Debug.Log("[Ogre] ROARS with fury!");
    }
}

/// <summary>
/// WaterCreature - יצור מים
/// </summary>
public class WaterCreature : EnemyCharacter
{
    private void Start()
    {
        characterName = "River Naga";
        difficulty = 2;
        maxHealth = 60;
        currentHealth = maxHealth;
        moveSpeed = 3.5f;
        Debug.Log("[Naga] Slithers through the water!");
    }
}

/// <summary>
/// BossEnemy - אויב בוס
/// </summary>
public class BossEnemy : EnemyCharacter
{
    [SerializeField] private bool hasShield = true;
    [SerializeField] private int specialAttackDamage = 50;

    private void Start()
    {
        characterName = "Shadow Lord";
        difficulty = 5;
        maxHealth = 300;
        currentHealth = maxHealth;
        moveSpeed = 2f;
        goldReward = 500;
        expReward = 1000;
        Debug.Log("[Shadow Lord] THE BOSS HAS AWAKENED!");
        Debug.Log("[Shadow Lord] Maximum Challenge Engaged!");
    }

    public void SpecialAttack(PlayerCharacter target)
    {
        Debug.Log("[Shadow Lord] UNLEASHING DARK POWER!");
        target.TakeDamage(specialAttackDamage);
    }
}
