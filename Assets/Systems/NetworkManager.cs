using System.Collections.Generic;
using UnityEngine;

namespace Ima.Systems
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance { get; private set; }

        public List<string> connectedPlayers = new List<string>();

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

        // Placeholder methods - integration point for real networking.
        public void AddPlayer(string id)
        {
            if (!connectedPlayers.Contains(id))
                connectedPlayers.Add(id);
        }

        public void RemovePlayer(string id)
        {
            if (connectedPlayers.Contains(id))
                connectedPlayers.Remove(id);
        }
    }
}