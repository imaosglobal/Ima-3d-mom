using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ima.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        private void Start()
        {
            // On first run, attempt to load the configured starting scene.
            string target = ConfigLoader.Config != null && !string.IsNullOrEmpty(ConfigLoader.Config.startingScene)
                ? ConfigLoader.Config.startingScene
                : "EyeEntry";

            // If target scene exists in build settings, load it; otherwise create runtime EyeEntry if requested.
            var scene = SceneManager.GetSceneByName(target);
            if (scene.IsValid())
            {
                if (SceneManager.GetActiveScene().name != target)
                    SceneManager.LoadScene(target);
            }
            else
            {
                if (target == "EyeEntry")
                {
                    CreateRuntimeEyeEntry();
                }
                else
                {
                    Debug.LogWarning($"[SceneBootstrap] Scene '{target}' not found in build settings. Staying in current scene.");
                }
            }
        }

        private void CreateRuntimeEyeEntry()
        {
            Debug.Log("[SceneBootstrap] Creating runtime EyeEntry scene...");

            // Camera
            if (Camera.main == null)
            {
                var camObj = new GameObject("MainCamera");
                var cam = camObj.AddComponent<Camera>();
                cam.clearFlags = CameraClearFlags.SolidColor;
                cam.backgroundColor = Color.black;
                camObj.tag = "MainCamera";
                camObj.transform.position = new Vector3(0, 0, -3);
            }

            // Golden eye
            var eyeObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            eyeObj.name = "GoldenEye";
            eyeObj.transform.position = new Vector3(0, 0, 2f);
            eyeObj.transform.localScale = Vector3.one * 0.8f;

            var eyeVisuals = eyeObj.AddComponent<global::Ima.EyeGateway.EyeVisuals>();
            eyeVisuals.eyeBody = eyeObj.transform;

            var eyeGateway = eyeObj.AddComponent<global::Ima.EyeGateway.EyeGateway>();
            eyeGateway.pupilTransform = eyeObj.transform;
            eyeGateway.irisTransform = eyeObj.transform;

            var controllerObj = new GameObject("EyeEntryController");
            var controller = controllerObj.AddComponent<global::Ima.EyeGateway.EyeEntryController>();
            controller.eye = eyeGateway;
            controller.playerCamera = Camera.main;

            // Ensure audio manager exists
            if (global::Ima.Core.AudioManager.Instance == null)
            {
                var amObj = new GameObject("AudioManager");
                amObj.AddComponent<global::Ima.Core.AudioManager>();
            }
        }
    }
}