using UnityEngine;

namespace Ima.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

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

        public bool GetInteractDown()
        {
            // Keyboard / mouse
            if (Input.GetMouseButtonDown(0)) return true;
            if (Input.GetKeyDown(KeyCode.E)) return true;

            // Touch
            if (Input.touchCount > 0)
            {
                var t = Input.touches[0];
                if (t.phase == TouchPhase.Began) return true;
            }

            return false;
        }

        public bool GetInteractHold()
        {
            if (Input.GetMouseButton(0)) return true;
            if (Input.GetKey(KeyCode.E)) return true;
            if (Input.touchCount > 0) return true;
            return false;
        }
    }
}