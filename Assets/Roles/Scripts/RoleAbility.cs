using UnityEngine;

namespace Ima.Roles
{
    [CreateAssetMenu(menuName = "Ima/RoleAbility", fileName = "RoleAbility")]
    public class RoleAbility : ScriptableObject
    {
        public string abilityId;
        public string displayName;
        public string description;
        public int requiredLevel = 1;
        public GameObject gearPrefab;
    }
}