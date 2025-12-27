using Ima.Roles;
using UnityEngine;

namespace Ima.UI
{
    public class RolesUI : MonoBehaviour
    {
        public RolesManager rolesManager;

        private void Start()
        {
            rolesManager = rolesManager ?? RolesManager.Instance;
        }

        public void ChooseRole(string roleName)
        {
            if (rolesManager == null) return;
            if (System.Enum.TryParse<RoleType>(roleName, out RoleType rt))
            {
                rolesManager.SetRole(rt);
            }
        }

        public string GetCurrentRoleName()
        {
            return rolesManager?.CurrentRole?.Role.ToString() ?? "None";
        }
    }
}