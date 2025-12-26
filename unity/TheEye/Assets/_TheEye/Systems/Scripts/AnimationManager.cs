using UnityEngine;

/// <summary>
/// AnimationManager - ניהול אנימציות דמויות
/// </summary>
public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("[AnimationManager] No Animator found on this GameObject");
        }
        Debug.Log("[AnimationManager] Initialized");
    }

    public void PlayWalkAnimation()
    {
        if (animator) animator.SetBool("IsWalking", true);
        Debug.Log("[Animation] Play: Walk");
    }

    public void PlayIdleAnimation()
    {
        if (animator) animator.SetBool("IsWalking", false);
        Debug.Log("[Animation] Play: Idle");
    }

    public void PlayAttackAnimation()
    {
        if (animator) animator.SetTrigger("Attack");
        Debug.Log("[Animation] Play: Attack");
    }

    public void PlayDeathAnimation()
    {
        if (animator) animator.SetTrigger("Die");
        Debug.Log("[Animation] Play: Death");
    }

    public void PlayHealAnimation()
    {
        if (animator) animator.SetTrigger("Heal");
        Debug.Log("[Animation] Play: Heal");
    }

    public void PlayCastAnimation()
    {
        if (animator) animator.SetTrigger("Cast");
        Debug.Log("[Animation] Play: Cast Spell");
    }

    public void PlayEmotionAnimation(string emotion)
    {
        if (animator) animator.SetTrigger(emotion);
        Debug.Log($"[Animation] Play: {emotion}");
    }
}
