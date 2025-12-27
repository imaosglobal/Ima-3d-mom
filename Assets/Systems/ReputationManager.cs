using System.Collections.Generic;
using UnityEngine;

namespace Ima.Systems
{
    public class ReputationManager : MonoBehaviour
    {
        public static ReputationManager Instance { get; private set; }

        private Dictionary<string, int> _reputations = new Dictionary<string, int>();

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

        public int GetReputation(string playerId)
        {
            if (_reputations.TryGetValue(playerId, out int rep)) return rep;
            return 0;
        }

        public void AdjustReputation(string playerId, int delta)
        {
            if (!_reputations.ContainsKey(playerId)) _reputations[playerId] = 0;
            _reputations[playerId] += delta;
        }
    }
}