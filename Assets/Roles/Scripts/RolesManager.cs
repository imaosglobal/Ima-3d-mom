using System;
using System.Collections.Generic;
using Ima.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Ima.Roles
{
    public enum RoleType { Hunter, Creator, Mystic }

    [Serializable]
    public class RoleProfile
    {
        public RoleType Role;
        public int Level;
        public List<string> UnlockedAbilities = new List<string>();
    }

    public class AbilityEvent : UnityEvent<string> {}

    public class RolesManager : MonoBehaviour
    {
        public static RolesManager Instance { get; private set; }

        public RoleProfile CurrentRole { get; private set; }

        public AbilityEvent OnAbilityUnlocked = new AbilityEvent();
        public UnityEvent OnRoleChanged = new UnityEvent();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Default start role
            CurrentRole = new RoleProfile { Role = RoleType.Hunter, Level = 1 };

            // Try to load saved role
            var sd = SaveSystem.Instance?.Load();
            if (sd != null)
            {
                Enum.TryParse(sd.role, out RoleType savedRole);
                CurrentRole.Role = savedRole;
                CurrentRole.Level = sd.roleLevel;
            }
        }

        public void SetRole(RoleType role)
        {
            CurrentRole.Role = role;
            CurrentRole.Level = 1;
            CurrentRole.UnlockedAbilities.Clear();
            OnRoleChanged?.Invoke();
            SaveSystem.Instance?.SaveRole(CurrentRole.Role.ToString(), CurrentRole.Level);
        }

        public void AddAbility(string ability)
        {
            if (!CurrentRole.UnlockedAbilities.Contains(ability))
            {
                CurrentRole.UnlockedAbilities.Add(ability);
                OnAbilityUnlocked?.Invoke(ability);
                SaveSystem.Instance?.SaveRole(CurrentRole.Role.ToString(), CurrentRole.Level);
            }
        }

        public void LevelUp()
        {
            CurrentRole.Level++;
            SaveSystem.Instance?.SaveRole(CurrentRole.Role.ToString(), CurrentRole.Level);
        }
    }
}