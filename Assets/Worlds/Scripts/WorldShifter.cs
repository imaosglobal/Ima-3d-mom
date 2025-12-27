using System.Collections;
using UnityEngine;

namespace Ima.Worlds
{
    public class WorldShifter : MonoBehaviour
    {
        public float cycleDuration = 60f; // seconds per zone shift
        private float _timer = 0f;
        private int _zoneIndex = 0;
        private string[] _zones = new string[] { "Forest", "Ruins", "Shifting" };

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= cycleDuration)
            {
                _timer = 0f;
                NextZone();
            }
        }

        public UnityEngine.Events.UnityEvent<string> OnZoneChanged = new UnityEngine.Events.UnityEvent<string>();

        private void NextZone()
        {
            _zoneIndex = (_zoneIndex + 1) % _zones.Length;
            var zoneName = _zones[_zoneIndex];
            Debug.Log("World shifted to zone: " + zoneName);
            // Notify listeners so systems can react
            OnZoneChanged?.Invoke(zoneName);
        }
    }
}