using Ima.Roles;
using UnityEngine;

namespace Ima.Worlds
{
    public class NPC : MonoBehaviour
    {
        public string npcName = "NPC";

        private void OnMouseDown()
        {
            ReactToPlayer();
        }

        public void ReactToPlayer()
        {
            var rm = RolesManager.Instance;
            string line = "Hello.";
            if (rm != null)
            {
                switch (rm.CurrentRole.Role)
                {
                    case RoleType.Hunter:
                        line = $"{npcName}: I hear you track well, hunter. Stay sharp!";
                        break;
                    case RoleType.Creator:
                        line = $"{npcName}: Builder! Your hands shape the land — don't forget to share.";
                        break;
                    case RoleType.Mystic:
                        line = $"{npcName}: The winds whisper to you. Do you listen?";
                        break;
                }
            }
            Debug.Log(line);
        }
    }
}