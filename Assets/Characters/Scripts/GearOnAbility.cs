using Ima.Roles;
using UnityEngine;

namespace Ima.Characters
{
    public class GearOnAbility : MonoBehaviour
    {
        public RoleAbility[] abilitiesToGear;

        private void Start()
        {
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
            foreach (var a in abilitiesToGear)
            {
                if (a != null && a.abilityId == abilityId && a.gearPrefab != null)
                {
                    var g = Instantiate(a.gearPrefab, transform);
                    g.name = abilityId + "_gear";
                }
            }
        }
    }
}