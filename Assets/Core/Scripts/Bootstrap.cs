using UnityEngine;

namespace Ima.Core
{
    [DefaultExecutionOrder(-100)]
    public class Bootstrap : MonoBehaviour
    {
        private static Bootstrap _instance;
        public static Bootstrap Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Minimal initialization hook and core singletons
            if (gameObject.GetComponent<SceneBootstrap>() == null) gameObject.AddComponent<SceneBootstrap>();
            if (gameObject.GetComponent<ConfigLoader>() == null) gameObject.AddComponent<ConfigLoader>();
            if (gameObject.GetComponent<InputManager>() == null) gameObject.AddComponent<InputManager>();
            if (gameObject.GetComponent<SaveSystem>() == null) gameObject.AddComponent<SaveSystem>();
            if (gameObject.GetComponent<GameSceneManager>() == null) gameObject.AddComponent<GameSceneManager>();
            if (gameObject.GetComponent<AudioManager>() == null) gameObject.AddComponent<AudioManager>();

            // Roles and systems
            if (gameObject.GetComponent<global::Ima.Roles.RolesManager>() == null) gameObject.AddComponent<global::Ima.Roles.RolesManager>();
            if (gameObject.GetComponent<global::Ima.Systems.NetworkManager>() == null) gameObject.AddComponent<global::Ima.Systems.NetworkManager>();
            if (gameObject.GetComponent<global::Ima.Systems.ReputationManager>() == null) gameObject.AddComponent<global::Ima.Systems.ReputationManager>();
            if (gameObject.GetComponent<global::Ima.Worlds.ScenesInitializer>() == null) gameObject.AddComponent<global::Ima.Worlds.ScenesInitializer>();
            if (gameObject.GetComponent<global::Ima.Worlds.WorldShifter>() == null) gameObject.AddComponent<global::Ima.Worlds.WorldShifter>();
            if (gameObject.GetComponent<global::Ima.Systems.EncounterManager>() == null) gameObject.AddComponent<global::Ima.Systems.EncounterManager>();
            if (gameObject.GetComponent<global::Ima.Worlds.CreatorBuildManager>() == null) gameObject.AddComponent<global::Ima.Worlds.CreatorBuildManager>();
        }
    }
}