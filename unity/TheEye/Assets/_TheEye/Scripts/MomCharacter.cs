using UnityEngine;

/// <summary>
/// MomCharacter - לוגיקת דמות אמא
/// </summary>
public class MomCharacter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;
    
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// טיפול בקלט משחקן
    /// </summary>
    private void HandleInput()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        // תנועה
        if (moveInput != 0)
        {
            Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }

        // סיבוב
        if (rotateInput != 0)
        {
            transform.Rotate(0, rotateInput * rotationSpeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// שינוי רגש/אנימציה
    /// </summary>
    public void SetEmotion(string emotion)
    {
        if (animator != null)
        {
            animator.SetTrigger(emotion);
            Debug.Log($"Mom emotion changed to: {emotion}");
        }
    }
}
