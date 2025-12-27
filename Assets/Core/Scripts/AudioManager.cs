using UnityEngine;

namespace Ima.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        private AudioSource _musicSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.playOnAwake = false;
            _musicSource.loop = true;
        }

        public void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, Camera.main != null ? Camera.main.transform.position : Vector3.zero, volume);
        }

        public void SetMusic(AudioClip clip, float volume = 1f)
        {
            if (_musicSource.clip == clip) return;
            _musicSource.clip = clip;
            _musicSource.volume = volume;
            if (clip != null)
                _musicSource.Play();
            else
                _musicSource.Stop();
        }
    }
}