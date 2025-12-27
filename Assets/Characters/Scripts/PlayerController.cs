using UnityEngine;

namespace Ima.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 3f;
        public float runSpeed = 6f;
        public float gravity = -9.81f;
        private CharacterController _cc;
        private Vector3 _velocity;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = transform.right * h + transform.forward * v;
            bool running = Input.GetKey(KeyCode.LeftShift);
            float speed = running ? runSpeed : walkSpeed;
            _cc.Move(dir * speed * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _cc.Move(_velocity * Time.deltaTime);

            if (_cc.isGrounded && _velocity.y < 0)
                _velocity.y = -2f;
        }
    }
}