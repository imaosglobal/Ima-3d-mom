using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CharacterSelectionUI - בחירת דמות בתחילת משחק
/// </summary>
public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private Button creatorBtn;
    [SerializeField] private Button hunterBtn;
    [SerializeField] private Button mageBtn;
    [SerializeField] private Button healerBtn;
    [SerializeField] private Button explorerBtn;

    private void Start()
    {
        Debug.Log("[CharacterSelectionUI] Select your character!");
        
        if (creatorBtn) creatorBtn.onClick.AddListener(() => SelectCharacter("Creator"));
        if (hunterBtn) hunterBtn.onClick.AddListener(() => SelectCharacter("Hunter"));
        if (mageBtn) mageBtn.onClick.AddListener(() => SelectCharacter("Mage"));
        if (healerBtn) healerBtn.onClick.AddListener(() => SelectCharacter("Healer"));
        if (explorerBtn) explorerBtn.onClick.AddListener(() => SelectCharacter("Explorer"));
    }

    private void SelectCharacter(string roleName)
    {
        Debug.Log($"[CharacterSelection] You chose: {roleName}");
        // ניתן להוסיף לוגיקה לטעינת הדמות הנבחרת
    }
}
