using UnityEngine;

/// <summary>
/// TutorialManager - מערכת הדרכה למשחקנים חדשים
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] private bool tutorialEnabled = true;
    [SerializeField] private int tutorialStep = 0;

    private bool tutorialCompleted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (tutorialEnabled && !tutorialCompleted)
        {
            StartTutorial();
        }
    }

    /// <summary>
    /// התחלת הדרכה
    /// </summary>
    private void StartTutorial()
    {
        Debug.Log("[Tutorial] Starting tutorial...");
        ShowTutorialStep(0);
    }

    /// <summary>
    /// הצגת שלב הדרכה
    /// </summary>
    private void ShowTutorialStep(int step)
    {
        tutorialStep = step;
        
        switch (step)
        {
            case 0:
                ShowMessage("Welcome to The Eye!");
                ShowMessage("You are a young adventurer choosing your path...");
                break;
            case 1:
                ShowMessage("Use WASD or Arrow Keys to move");
                ShowMessage("Press Space to jump");
                break;
            case 2:
                ShowMessage("Press E to interact with NPCs");
                ShowMessage("Talk to them to get quests!");
                break;
            case 3:
                ShowMessage("Press I for Inventory");
                ShowMessage("Press Q for Quests");
                break;
            case 4:
                ShowMessage("Defeat enemies to gain XP and Gold");
                ShowMessage("Build your strength and complete quests!");
                break;
            case 5:
                ShowMessage("Your mother (Ima) will always be there for you");
                ShowMessage("Listen to her guidance...");
                break;
            default:
                SkipTutorial();
                break;
        }
    }

    /// <summary>
    /// קדום לשלב הבא
    /// </summary>
    public void NextTutorialStep()
    {
        tutorialStep++;
        if (tutorialStep >= 6)
        {
            SkipTutorial();
        }
        else
        {
            ShowTutorialStep(tutorialStep);
        }
    }

    /// <summary>
    /// דלג על הדרכה
    /// </summary>
    public void SkipTutorial()
    {
        tutorialCompleted = true;
        Debug.Log("[Tutorial] Tutorial completed!");
        // Close tutorial UI placeholder - in-Unity, attach UI manager to respond to this state
        tutorialEnabled = false;
        ShowMessage("Tutorial UI closed.");
    }

    /// <summary>
    /// הצג הודעה
    /// </summary>
    private void ShowMessage(string message)
    {
        Debug.Log($"[Tutorial] {message}");
    }

    public bool IsTutorialActive() => tutorialEnabled && !tutorialCompleted;
}
