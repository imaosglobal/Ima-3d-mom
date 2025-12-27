using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ima.Core
{
    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance { get; private set; }

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

        public void Load(string sceneName)
        {
            StartCoroutine(LoadAsync(sceneName));
        }

        private IEnumerator LoadAsync(string sceneName)
        {
            var op = SceneManager.LoadSceneAsync(sceneName);
            while (!op.isDone)
                yield return null;
        }
    }
}