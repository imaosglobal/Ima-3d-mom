using UnityEngine;

namespace Ima.EyeGateway
{
    public class EyeVisuals : MonoBehaviour
    {
        public Transform eyeBody;
        public float idleBobAmount = 0.02f;
        public float idleBobSpeed = 1f;
        public float subtleScale = 0.005f;

        private Vector3 _startPos;
        private Vector3 _startScale;

        void Start()
        {
            if (eyeBody == null) eyeBody = transform;
            _startPos = eyeBody.localPosition;
            _startScale = eyeBody.localScale;
        }

        void Update()
        {
            float t = Time.time * idleBobSpeed;
            eyeBody.localPosition = _startPos + new Vector3(0f, Mathf.Sin(t) * idleBobAmount, 0f);
            eyeBody.localScale = _startScale * (1f + Mathf.Sin(t * 0.5f) * subtleScale);
        }
    }
}