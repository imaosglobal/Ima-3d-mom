using UnityEngine;

/// <summary>
/// CharacterBase - בסיס לכל הדמויות במשחק
/// </summary>
public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected string characterName = "Character";
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int level = 1;
    [SerializeField] protected int experience = 0;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotationSpeed = 180f;
    
    protected Animator animator;
    protected Rigidbody rb;
    protected bool isAlive = true;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        Debug.Log($"[{characterName}] Initialized - Health: {currentHealth}/{maxHealth}");
    }

    /// <summary>
    /// נזק לדמות
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"[{characterName}] Took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// הוספת XP וטיפול בלבל אפ
    /// </summary>
    public virtual void AddExperience(int exp)
    {
        experience += exp;
        Debug.Log($"[{characterName}] +{exp} XP (Total: {experience})");
        CheckLevelUp();
    }

    /// <summary>
    /// בדיקה האם לעלות רמה
    /// </summary>
    protected virtual void CheckLevelUp()
    {
        int requiredExp = level * 100;
        if (experience >= requiredExp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// עלייה בדרגה
    /// </summary>
    protected virtual void LevelUp()
    {
        level++;
        maxHealth += 20;
        currentHealth = maxHealth;
        Debug.Log($"[{characterName}] LEVEL UP! New Level: {level}");
    }

    /// <summary>
    /// מוות הדמות
    /// </summary>
    public virtual void Die()
    {
        isAlive = false;
        Debug.Log($"[{characterName}] has died!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        // ניתן להוסיף יותר לוגיקה מוות בהמשך
    }

    public int GetHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public int GetLevel() => level;
    public int GetExperience() => experience;
    public bool IsAlive() => isAlive;

    // Setters to support save/load
    public void SetHealth(int h)
    {
        currentHealth = Mathf.Clamp(h, 0, maxHealth);
        if (currentHealth <= 0) Die();
    }

    public void SetLevel(int newLevel)
    {
        if (newLevel <= 0) return;
        level = newLevel;
        maxHealth = 100 + (level - 1) * 20;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }
}
