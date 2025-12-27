using UnityEngine;

namespace Ima.Core
{
    [System.Serializable]
    public class ProjectConfig
    {
        public string projectName;
        public string startingScene;
        public string[] roles;
    }

    public class ConfigLoader : MonoBehaviour
    {
        public static ProjectConfig Config { get; private set; }

        private void Awake()
        {
            if (Config != null) return;
            Load();
            DontDestroyOnLoad(this.gameObject);
        }

        public void Load()
        {
            var ta = Resources.Load<TextAsset>("config");
            if (ta != null)
            {
                try
                {
                    Config = JsonUtility.FromJson<ProjectConfig>(ta.text);
                }
                catch
                {
                    Debug.LogWarning("Failed to parse config.json");
                }
            }
            else
            {
                Debug.LogWarning("config.json not found in Resources");
            }
        }
    }
}