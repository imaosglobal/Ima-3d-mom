using Ima.Roles;
using UnityEngine;

namespace Ima.Characters
{
    public class PlayerAbilityEffects : MonoBehaviour
    {
        private PlayerController _pc;

        private void Start()
        {
            _pc = GetComponent<PlayerController>();
            if (RolesManager.Instance != null)
                RolesManager.Instance.OnAbilityUnlocked.AddListener(OnAbilityUnlocked);
        }

        private void OnDestroy()
        {
            if (RolesManager.Instance != null)
                RolesManager.Instance.OnAbilityUnlocked.RemoveListener(OnAbilityUnlocked);
        }

        private void OnAbilityUnlocked(string abilityId)
        {
            switch (abilityId)
            {
                case "hunter_sprint":
                    if (_pc != null) _pc.runSpeed *= 1.25f;
                    break;
                case "mystic_perception":
                    // For now, log; visual highlight integration point
                    Debug.Log("Mystic perception unlocked: enable highlight system");
                    break;
                case "creator_build":
                    Debug.Log("Creator build unlocked: enable build tools");
                    break;
            }
        }
    }
}