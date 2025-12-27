using System;
using UnityEngine;

namespace Ima.Core
{
    [Serializable]
    public class SaveData
    {
        public string role;
        public int roleLevel;
    }

    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }

        private const string SaveKey = "Ima_TheEye_Save_v1";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void SaveRole(string roleName, int level)
        {
            var sd = new SaveData { role = roleName, roleLevel = level };
            var json = JsonUtility.ToJson(sd);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return null;
            var json = PlayerPrefs.GetString(SaveKey);
            try
            {
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch
            {
                Debug.LogWarning("Failed to parse save data");
                return null;
            }
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();
        }
    }
}