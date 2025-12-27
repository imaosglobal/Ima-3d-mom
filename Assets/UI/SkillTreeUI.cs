using System.Collections.Generic;
using Ima.Roles;
using UnityEngine;

namespace Ima.UI
{
    public class SkillTreeUI : MonoBehaviour
    {
        public RolesManager rolesManager;

        private void Start()
        {
            if (rolesManager == null) rolesManager = RolesManager.Instance;
        }

        public void UnlockAbility(string ability)
        {
            rolesManager?.AddAbility(ability);
            // Visual feedback hook - in Unity attach animations, icons etc.
        }

        public IEnumerable<string> GetUnlockedAbilities()
        {
            return rolesManager?.CurrentRole?.UnlockedAbilities ?? new List<string>();
        }
    }
}