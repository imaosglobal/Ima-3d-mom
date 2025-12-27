using System.Collections;
using Ima.Core;
using UnityEngine;

namespace Ima.EyeGateway
{
    public class EyeEntryController : MonoBehaviour
    {
        public EyeGateway eye;
        public Camera playerCamera;

        private bool _isFocusing = false;

        private void Start()
        {
            if (playerCamera == null && Camera.main != null)
                playerCamera = Camera.main;
        }

        private void Update()
        {
            if (eye == null) return;

            // basic focus detection using raycast from camera center
            var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20f))
            {
                if (hit.collider != null && hit.collider.gameObject == eye.gameObject)
                {
                    _isFocusing = true;
                }
                else
                {
                    _isFocusing = false;
                }
            }
            else
            {
                _isFocusing = false;
            }

            if (_isFocusing)
            {
                if (InputManager.Instance != null && InputManager.Instance.GetInteractHold())
                {
                    // Start entry if player holds interact
                    StartCoroutine(EnterSequence());
                }
            }
        }

        private IEnumerator EnterSequence()
        {
            // prevent multiple triggers
            enabled = false;

            if (eye != null)
            {
                yield return StartCoroutine(eye.EnterThroughPupil(playerCamera));
            }

            // Prepare a screen fade
            Ima.UI.ScreenFader fader = FindObjectOfType<Ima.UI.ScreenFader>();
            if (fader == null)
            {
                var faderObj = new GameObject("ScreenFader");
                fader = faderObj.AddComponent<Ima.UI.ScreenFader>();
                DontDestroyOnLoad(faderObj);
            }

            yield return StartCoroutine(fader.FadeOut(0.8f));

            // After transition, load main world/hub
            GameSceneManager.Instance.Load(ConfigLoader.Config != null ? ConfigLoader.Config.startingScene ?? "Hub" : "Hub");

            // Fade in once the new scene is loaded
            yield return new WaitForSeconds(0.4f);
            yield return StartCoroutine(fader.FadeIn(0.9f));
        }
    }
}