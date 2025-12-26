using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainMenuUI - תפריט ראשי
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;

    private void Start()
    {
        Debug.Log("[MainMenuUI] Main Menu loaded");
        
        if (newGameBtn) newGameBtn.onClick.AddListener(() => StartNewGame());
        if (continueBtn) continueBtn.onClick.AddListener(() => ContinueGame());
        if (settingsBtn) settingsBtn.onClick.AddListener(() => OpenSettings());
        if (quitBtn) quitBtn.onClick.AddListener(() => QuitGame());
    }

    private void StartNewGame()
    {
        Debug.Log("[MainMenu] Starting new game...");
        // ניתן להעביר לסצנת בחירת דמות
    }

    private void ContinueGame()
    {
        Debug.Log("[MainMenu] Continuing previous game...");
        // ניתן להעביר לסצנה האחרונה
    }

    private void OpenSettings()
    {
        Debug.Log("[MainMenu] Opening settings...");
    }

    private void QuitGame()
    {
        Debug.Log("[MainMenu] Quitting game...");
        Application.Quit();
    }
}
