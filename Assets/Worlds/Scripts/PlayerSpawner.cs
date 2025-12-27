using Ima.Characters;
using UnityEngine;

namespace Ima.Worlds
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Vector3 spawnPosition = new Vector3(0f, 1f, -3f);

        private void Start()
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                SpawnPlayer();
            }
        }

        public void SpawnPlayer()
        {
            var player = new GameObject("Player");
            player.tag = "Player";
            var cc = player.AddComponent<CharacterController>();
            cc.height = 1.8f;
            cc.radius = 0.3f;
            player.transform.position = spawnPosition;

            player.AddComponent<PlayerController>();

            var cam = new GameObject("MainCamera");
            cam.transform.parent = player.transform;
            cam.transform.localPosition = new Vector3(0f, 1.2f, 0f);
            var cameraComp = cam.AddComponent<Camera>();
            cameraComp.tag = "MainCamera";

            cam.AddComponent<AudioListener>();
        }
    }
}