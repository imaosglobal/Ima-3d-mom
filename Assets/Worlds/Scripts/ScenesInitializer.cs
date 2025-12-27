using System.Collections.Generic;
using Ima.Roles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ima.Worlds
{
    public class ScenesInitializer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Hub")
            {
                SetupHub();
            }
            else if (scene.name == "EyeEntry")
            {
                // EyeEntry is expected to be authored; minimal runtime check
            }
        }

        private void SetupHub()
        {
            // Basic runtime-generated Ancient Village hub using primitives
            var root = new GameObject("AncientVillageRoot");

            // Ground
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.transform.parent = root.transform;
            ground.transform.localScale = new Vector3(5f, 1f, 5f);
            ground.name = "VillageGround";

            // Fire pit (visual)
            var fire = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fire.transform.parent = root.transform;
            fire.transform.position = new Vector3(0f, 0.5f, 2f);
            fire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            fire.name = "FirePit";

            // Simple NPCs
            CreateNPC(new Vector3(2f, 0f, 1f), "Mila");
            CreateNPC(new Vector3(-2f, 0f, -1f), "Tov");

            // Lighting
            var lightObj = new GameObject("HubLight");
            var light = lightObj.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 0.8f;
            lightObj.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            // Player spawner
            var spawner = root.AddComponent<PlayerSpawner>();
            spawner.SpawnPlayer();
        }

        private void CreateNPC(Vector3 pos, string name)
        {
            var npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            npc.transform.position = pos;
            npc.name = "NPC_" + name;
            var comp = npc.AddComponent<NPC>();
            comp.npcName = name;
        }
    }
}