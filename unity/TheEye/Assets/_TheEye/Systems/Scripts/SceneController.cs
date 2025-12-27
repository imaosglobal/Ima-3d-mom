using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneManager - ניהול מעברי סצנות
/// </summary>
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [SerializeField] private bool loadingScreenEnabled = true;
    private string currentScene = "";
    private string nextScene = "";

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
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"[SceneController] Current scene: {currentScene}");
    }

    /// <summary>
    /// טעינת סצנה חדשה
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (currentScene == sceneName)
        {
            Debug.LogWarning($"[SceneController] Already in {sceneName}!");
            return;
        }

        nextScene = sceneName;
        Debug.Log($"[SceneController] Loading scene: {sceneName}");

        if (loadingScreenEnabled)
        {
            ShowLoadingScreen();
        }

        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;

        Debug.Log($"[SceneController] Scene loaded: {sceneName}");
    }

    /// <summary>
    /// טעינת סצנה עם Async
    /// </summary>
    public void LoadSceneAsync(string sceneName)
    {
        if (currentScene == sceneName)
        {
            Debug.LogWarning($"[SceneController] Already in {sceneName}!");
            return;
        }

        nextScene = sceneName;
        Debug.Log($"[SceneController] Loading scene (Async): {sceneName}");

        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private System.Collections.IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        if (loadingScreenEnabled)
        {
            ShowLoadingScreen();
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
        }

        currentScene = sceneName;
        Debug.Log($"[SceneController] Scene loaded (Async): {sceneName}");
    }

    private void ShowLoadingScreen()
    {
        Debug.Log("[SceneController] Loading screen shown (placeholder)");
        // Placeholder: in-Unity, show a loading UI canvas and animate progress
    }

    /// <summary>
    /// חזרה לתפריט ראשי
    /// </summary>
    public void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }

    /// <summary>
    /// טען סצנה כפר
    /// </summary>
    public void LoadVillage()
    {
        LoadScene("Village");
    }

    /// <summary>
    /// טען סצנה יער
    /// </summary>
    public void LoadForest()
    {
        LoadScene("Forest");
    }

    /// <summary>
    /// טען סצנה מערה
    /// </summary>
    public void LoadCave()
    {
        LoadScene("Cave");
    }

    /// <summary>
    /// טען סצנה נהר
    /// </summary>
    public void LoadRiver()
    {
        LoadScene("River");
    }

    public string GetCurrentScene() => currentScene;
    public string GetNextScene() => nextScene;
}
