using UnityEngine;

/// <summary>
/// AudioManager - מנהל סאונד ומוזיקה
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float masterVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        Debug.Log("[AudioManager] Initialized");
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.volume = 0.6f * masterVolume;
            musicSource.Play();
            Debug.Log($"[Music] Playing: {clip.name}");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.volume = masterVolume;
            sfxSource.PlayOneShot(clip);
            Debug.Log($"[SFX] Playing: {clip.name}");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            Debug.Log("[Music] Stopped");
        }
    }

    public void SetVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        if (musicSource) musicSource.volume = 0.6f * masterVolume;
        if (sfxSource) sfxSource.volume = masterVolume;
        Debug.Log($"[Audio] Volume: {masterVolume * 100}%");
    }
}
