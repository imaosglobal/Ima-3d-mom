using Ima.Roles;
using UnityEngine;

namespace Ima.Worlds
{
    public class EnemyAI : MonoBehaviour
    {
        public float baseHealth = 10f;
        public float baseDamage = 2f;
        private float _health;

        private void Start()
        {
            AdaptToPlayerRole();
        }

        public void AdaptToPlayerRole()
        {
            var rm = RolesManager.Instance;
            if (rm == null) return;

            _health = baseHealth;
            switch (rm.CurrentRole.Role)
            {
                case RoleType.Hunter:
                    _health *= 1.1f + rm.CurrentRole.Level * 0.05f;
                    break;
                case RoleType.Creator:
                    _health *= 0.9f;
                    break;
                case RoleType.Mystic:
                    _health *= 1.0f + rm.CurrentRole.Level * 0.03f;
                    break;
            }
        }

        public void TakeDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}