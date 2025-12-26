using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// EnemyCharacter - דמות אויב עם AI בסיסי
/// </summary>
public class EnemyCharacter : CharacterBase
{
    [SerializeField] private int difficulty = 1; // 1-Easy, 2-Medium, 3-Hard
    [SerializeField] private int goldReward = 10;
    [SerializeField] private int expReward = 50;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;

    private float lastAttackTime = 0;
    private PlayerCharacter target;
    private bool isAggro = false;

    protected override void Start()
    {
        base.Start();
        // התאמה של תכונות לפי רמת קושי
        maxHealth = 30 * difficulty;
        currentHealth = maxHealth;
        moveSpeed = 3f + (difficulty * 0.5f);
        goldReward = 10 * difficulty;
        expReward = 50 * difficulty;
        Debug.Log($"[Enemy: {characterName}] Difficulty: {difficulty}, Health: {maxHealth}");
    }

    private void Update()
    {
        if (!isAlive) return;

        FindTarget();
        if (target != null)
        {
            PatrolAndAttack();
        }
    }

    /// <summary>
    /// חיפוש שחקן בטווח
    /// </summary>
    private void FindTarget()
    {
        if (target == null || !target.IsAlive())
        {
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
        }

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            isAggro = distance <= detectionRange;
        }
    }

    /// <summary>
    /// התנהגות האויב
    /// </summary>
    private void PatrolAndAttack()
    {
        if (!isAggro) return;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        // תנועה לכיוון השחקן
        if (distance > attackRange)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(target.transform);
        }

        // התקפה
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackTarget();
            lastAttackTime = Time.time;
        }
    }

    /// <summary>
    /// התקפה על השחקן
    /// </summary>
    private void AttackTarget()
    {
        int damage = 10 * difficulty;
        target.TakeDamage(damage);
        Debug.Log($"[Enemy: {characterName}] Attacks player for {damage} damage!");
        
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// מוות עם תגמול
    /// </summary>
    public override void Die()
    {
        base.Die();
        if (target != null)
        {
            target.AddExperience(expReward);
            target.AddGold(goldReward);
            Debug.Log($"[Enemy] Dropped {goldReward} gold and {expReward} XP");
        }
        Destroy(gameObject, 2f);
    }
}
