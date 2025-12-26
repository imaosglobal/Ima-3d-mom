using UnityEngine;

/// <summary>
/// PlayerCharacter - דמות השחקן עם תת-מחלקות לכל תפקיד
/// </summary>
public class PlayerCharacter : CharacterBase
{
    [SerializeField] private CharacterRole role = CharacterRole.Creator;
    [SerializeField] private Inventory inventory;
    private int gold = 0;

    protected override void Start()
    {
        base.Start();
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }
        Debug.Log($"[Player] Role: {role} initialized");
    }

    private void Update()
    {
        if (isAlive)
        {
            HandleInput();
        }
    }

    /// <summary>
    /// טיפול בקלט של השחקן
    /// </summary>
    private void HandleInput()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        // תנועה
        if (moveInput != 0)
        {
            Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
            if (rb != null)
            {
                rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            }
        }

        // סיבוב
        if (rotateInput != 0)
        {
            transform.Rotate(0, rotateInput * rotationSpeed * Time.deltaTime, 0);
        }

        // יכולות דמות
        HandleRoleAbilities();
    }

    /// <summary>
    /// טיפול ביכולות על פי התפקיד
    /// </summary>
    private void HandleRoleAbilities()
    {
        switch (role)
        {
            case CharacterRole.Creator:
                if (Input.GetKeyDown(KeyCode.E)) CastCreatorAbility();
                break;
            case CharacterRole.Hunter:
                if (Input.GetKeyDown(KeyCode.E)) CastHunterAbility();
                break;
            case CharacterRole.Mage:
                if (Input.GetKeyDown(KeyCode.E)) CastMageAbility();
                break;
            case CharacterRole.Healer:
                if (Input.GetKeyDown(KeyCode.E)) CastHealerAbility();
                break;
            case CharacterRole.Explorer:
                if (Input.GetKeyDown(KeyCode.E)) CastExplorerAbility();
                break;
        }
    }

    private void CastCreatorAbility() => Debug.Log("[Creator] Using ability: Build/Create");
    private void CastHunterAbility() => Debug.Log("[Hunter] Using ability: Aim/Shoot");
    private void CastMageAbility() => Debug.Log("[Mage] Using ability: Cast Spell");
    private void CastHealerAbility() => Debug.Log("[Healer] Using ability: Heal");
    private void CastExplorerAbility() => Debug.Log("[Explorer] Using ability: Sense");

    public void AddGold(int amount) => gold += amount;
    public int GetGold() => gold;
    public CharacterRole GetRole() => role;
}

public enum CharacterRole
{
    Creator,
    Hunter,
    Mage,
    Healer,
    Explorer
}
