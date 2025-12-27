using System.Collections.Generic;
using Ima.Core;
using UnityEngine;

namespace Ima.Worlds
{
    public class CreatorBuildManager : MonoBehaviour
    {
        public static CreatorBuildManager Instance { get; private set; }

        public List<string> unlockedZones = new List<string>();

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

        public void UnlockZone(string zoneId)
        {
            if (!unlockedZones.Contains(zoneId))
            {
                unlockedZones.Add(zoneId);
                Debug.Log("Zone unlocked: " + zoneId);
                // Persist unlock state - use SaveSystem as needed
            }
        }

        public bool IsUnlocked(string zoneId) => unlockedZones.Contains(zoneId);
    }
}