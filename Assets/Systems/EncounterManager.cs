using Ima.Systems;
using UnityEngine;

namespace Ima.Systems
{
    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager Instance { get; private set; }

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

        public enum EncounterType { Neutral, Cooperative, Hostile }

        public EncounterType DetermineEncounter(string otherPlayerId, string localPlayerId)
        {
            var rep = ReputationManager.Instance?.GetReputation(otherPlayerId) ?? 0;
            if (rep > 10) return EncounterType.Cooperative;
            if (rep < -10) return EncounterType.Hostile;
            return EncounterType.Neutral;
        }
    }
}