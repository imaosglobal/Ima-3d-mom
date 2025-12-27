using System.Collections;
using Ima.Core;
using UnityEngine;

namespace Ima.EyeGateway
{
    [RequireComponent(typeof(Collider))]
    public class EyeGateway : MonoBehaviour
    {
        [Header("Eye Settings")]
        public Transform pupilTransform;
        public Transform irisTransform;
        public AudioClip imaVoiceLine;

        [Range(0.1f, 3f)]
        public float focusSpeed = 1f;

        private AudioSource _audio;
        private bool _isFocused = false;

        private void Awake()
        {
            _audio = gameObject.AddComponent<AudioSource>();
            _audio.playOnAwake = false;
            _audio.clip = imaVoiceLine;
        }

        private void OnMouseEnter()
        {
            StartCoroutine(SubtleIrisMovement());
            _isFocused = true;
            if (!_audio.isPlaying && imaVoiceLine != null)
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayOneShot(imaVoiceLine, 1f);
                else
                    _audio.Play();
            }
        }

        private void OnMouseExit()
        {
            _isFocused = false;
        }

        private IEnumerator SubtleIrisMovement()
        {
            float t = 0f;
            while (_isFocused)
            {
                t += Time.deltaTime * focusSpeed;
                if (irisTransform)
                {
                    irisTransform.localRotation = Quaternion.Euler(Mathf.Sin(t) * 2f, Mathf.Cos(t) * 2f, 0f);
                }
                yield return null;
            }
        }

        // Called externally when player holds interact while focused
        public IEnumerator EnterThroughPupil(Camera playerCamera)
        {
            // Simple pupil dilation and fov zoom transition
            float duration = 1.2f;
            float el = 0f;
            float initialFOV = playerCamera.fieldOfView;
            while (el < duration)
            {
                el += Time.deltaTime;
                float ratio = el / duration;
                if (pupilTransform)
                    pupilTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2f, ratio);
                playerCamera.fieldOfView = Mathf.Lerp(initialFOV, initialFOV * 1.6f, ratio);
                yield return null;
            }
        }
    }
}