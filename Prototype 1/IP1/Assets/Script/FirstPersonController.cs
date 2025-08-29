using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Mouse Control")]
    public float mouseSensitivity = 2f;
    public float upDownRange = 60f;
    
    [Header("Movement Control")]
    public float moveSpeed = 5f;
    public bool enableMovement = true;
    
    private float verticalRotation = 0;
    private Camera playerCamera;
    
    void Start()
    {
        // Get camera component
        playerCamera = GetComponentInChildren<Camera>();
        
        // Keep cursor visible for UI interaction (optional: lock for immersion)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    void Update()
    {
        // Only rotate camera when right mouse button is held
        if (Input.GetMouseButton(1))
        {
            RotateCamera();
        }
        
        if (enableMovement)
        {
            MovePlayer();
        }
    }
    
    void RotateCamera()
    {
        // Horizontal rotation (turn left/right)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
        
        // Vertical rotation (look up/down)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    
    void MovePlayer()
    {
        // WASD movement
        float horizontal = Input.GetAxis("Horizontal"); // A,D
        float vertical = Input.GetAxis("Vertical");     // W,S
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = transform.TransformDirection(direction);
        direction *= moveSpeed * Time.deltaTime;
        
        transform.Translate(direction, Space.World);
    }
    
    // Toggle cursor lock state (ESC key)
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}