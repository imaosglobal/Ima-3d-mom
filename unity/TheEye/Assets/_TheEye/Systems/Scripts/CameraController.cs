using UnityEngine;

/// <summary>
/// CameraController - ניהול מצלמה במשחק
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float lookAhead = 2f;
    [SerializeField] private float smoothness = 0.1f;
    [SerializeField] private float rotationSpeed = 100f;

    private Vector3 velocity = Vector3.zero;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        Debug.Log("[CameraController] Initialized");
    }

    private void LateUpdate()
    {
        if (target == null) return;

        HandleInput();
        UpdateCameraPosition();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            rotationX += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, -30f, 60f);
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 desiredPosition = target.position 
            - target.forward * distance 
            + Vector3.up * height;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothness
        );

        Vector3 lookAtPosition = target.position + Vector3.up * (height * 0.5f);
        transform.LookAt(lookAtPosition);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("[CameraController] Target changed");
    }

    public void SetDistance(float newDistance)
    {
        distance = newDistance;
    }

    public void SetHeight(float newHeight)
    {
        height = newHeight;
    }
}
