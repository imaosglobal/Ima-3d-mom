using UnityEngine;

/// <summary>
/// ParticleEffectManager - מנהל אפקטים חזותיים
/// </summary>
public class ParticleEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackEffectPrefab;
    [SerializeField] private ParticleSystem healEffectPrefab;
    [SerializeField] private ParticleSystem spellEffectPrefab;
    [SerializeField] private ParticleSystem deathEffectPrefab;

    public void PlayAttackEffect(Vector3 position)
    {
        if (attackEffectPrefab)
        {
            Instantiate(attackEffectPrefab, position, Quaternion.identity);
            Debug.Log("[Particle] Attack effect played");
        }
    }

    public void PlayHealEffect(Vector3 position)
    {
        if (healEffectPrefab)
        {
            Instantiate(healEffectPrefab, position, Quaternion.identity);
            Debug.Log("[Particle] Heal effect played");
        }
    }

    public void PlaySpellEffect(Vector3 position)
    {
        if (spellEffectPrefab)
        {
            Instantiate(spellEffectPrefab, position, Quaternion.identity);
            Debug.Log("[Particle] Spell effect played");
        }
    }

    public void PlayDeathEffect(Vector3 position)
    {
        if (deathEffectPrefab)
        {
            Instantiate(deathEffectPrefab, position, Quaternion.identity);
            Debug.Log("[Particle] Death effect played");
        }
    }
}
