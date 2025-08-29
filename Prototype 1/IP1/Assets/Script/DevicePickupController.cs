using UnityEngine;
using TMPro;

public class DevicePickupController : MonoBehaviour
{
    [Header("Device Settings")]
    public GameObject[] devices; // iPhone, iPad, Laptop
    public string[] deviceNames = {"iPhone", "iPad", "Laptop"};
    public Vector3[] originalPositions;
    public Quaternion[] originalRotations;
    
    [Header("FPS Style Hand Hold Settings")]
    public Transform handHoldPoint; // Empty GameObject as child of Player camera
    public Vector3[] fpsHoldPositions; // FPS style positions (bottom-right screen)
    public Vector3[] fpsHoldRotations; // FPS style rotations
    public Vector3[] fpsHoldScales; // Different scales for FPS view
    
    [Header("Animation Settings")]
    public float pickupAnimationSpeed = 3f;
    public AnimationCurve pickupCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("UI Feedback")]
    public TextMeshProUGUI deviceStatusText;
    
    [Header("Interaction")]
    public float pickupRange = 3f;
    public Camera playerCamera;
    
    private int currentDeviceIndex = -1; // -1 means no device held
    private bool isHoldingDevice = false;
    private bool isAnimatingPickup = false;
    private float animationTimer = 0f;
    
    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
            
        // Store original positions and rotations
        originalPositions = new Vector3[devices.Length];
        originalRotations = new Quaternion[devices.Length];
        
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i] != null)
            {
                originalPositions[i] = devices[i].transform.position;
                originalRotations[i] = devices[i].transform.rotation;
                
                // Add tags to devices
                devices[i].tag = $"Device_{i}";
            }
        }
        
        // Create hand hold point if doesn't exist
        if (handHoldPoint == null)
        {
            GameObject holdPoint = new GameObject("HandHoldPoint");
            holdPoint.transform.SetParent(playerCamera.transform);
            holdPoint.transform.localPosition = Vector3.zero;
            handHoldPoint = holdPoint.transform;
        }
        
        // Set FPS style positions (bottom-right of screen view)
        if (fpsHoldPositions.Length == 0)
        {
            fpsHoldPositions = new Vector3[]
            {
                new Vector3(0.4f, -0.4f, 0.6f),  // iPhone - bottom right
                new Vector3(0.3f, -0.3f, 0.7f),  // iPad - slightly more centered
                new Vector3(0.2f, -0.5f, 0.8f)   // Laptop - lower position
            };
        }
        
        if (fpsHoldRotations.Length == 0)
        {
            fpsHoldRotations = new Vector3[]
            {
                new Vector3(-10, -20, 5),   // iPhone - slight tilt
                new Vector3(-15, -15, 10),  // iPad - more angled
                new Vector3(-25, -10, 0)    // Laptop - angled down
            };
        }
        
        if (fpsHoldScales.Length == 0)
        {
            fpsHoldScales = new Vector3[]
            {
                new Vector3(1.2f, 1.2f, 1.2f),  // iPhone - slightly bigger
                new Vector3(1.0f, 1.0f, 1.0f),  // iPad - normal size
                new Vector3(0.8f, 0.8f, 0.8f)   // Laptop - smaller to fit screen
            };
        }
        
        UpdateDeviceStatusUI();
    }
    
    void Update()
    {
        HandleDeviceInteraction();
        HandleDeviceReturn();
        UpdatePickupAnimation();
    }
    
    void HandleDeviceInteraction()
    {
        if (Input.GetMouseButtonDown(0) && !isHoldingDevice && !isAnimatingPickup)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                // Check if hit a device
                for (int i = 0; i < devices.Length; i++)
                {
                    if (hit.collider.transform.IsChildOf(devices[i].transform))
                    {
                        StartPickupDevice(i);
                        break;
                    }
                }
            }
        }
    }
    
    void HandleDeviceReturn()
    {
        if (Input.GetKeyDown(KeyCode.R) && isHoldingDevice && !isAnimatingPickup)
        {
            ReturnDevice();
        }
    }
    
    void StartPickupDevice(int deviceIndex)
    {
        if (deviceIndex < 0 || deviceIndex >= devices.Length) return;
        
        currentDeviceIndex = deviceIndex;
        isAnimatingPickup = true;
        animationTimer = 0f;
        
        Debug.Log($"Starting pickup animation for {deviceNames[deviceIndex]}");
    }
    
    void UpdatePickupAnimation()
    {
        if (!isAnimatingPickup) return;
        
        animationTimer += Time.deltaTime * pickupAnimationSpeed;
        float t = pickupCurve.Evaluate(Mathf.Clamp01(animationTimer));
        
        GameObject device = devices[currentDeviceIndex];
        Vector3 startPos = originalPositions[currentDeviceIndex];
        Quaternion startRot = originalRotations[currentDeviceIndex];
        Vector3 startScale = Vector3.one;
        
        // Target FPS position relative to camera
        Vector3 targetWorldPos = playerCamera.transform.TransformPoint(fpsHoldPositions[currentDeviceIndex]);
        Quaternion targetRot = playerCamera.transform.rotation * Quaternion.Euler(fpsHoldRotations[currentDeviceIndex]);
        Vector3 targetScale = fpsHoldScales[currentDeviceIndex];
        
        // Animate position, rotation, and scale
        device.transform.position = Vector3.Lerp(startPos, targetWorldPos, t);
        device.transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
        device.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        
        // Animation complete
        if (animationTimer >= 1f)
        {
            CompletePickup();
        }
    }
    
    void CompletePickup()
    {
        isAnimatingPickup = false;
        isHoldingDevice = true;
        
        GameObject device = devices[currentDeviceIndex];
        
        // Parent to camera for FPS movement
        device.transform.SetParent(playerCamera.transform);
        device.transform.localPosition = fpsHoldPositions[currentDeviceIndex];
        device.transform.localRotation = Quaternion.Euler(fpsHoldRotations[currentDeviceIndex]);
        device.transform.localScale = fpsHoldScales[currentDeviceIndex];
        
        // Disable device's main collider
        Collider deviceCollider = device.GetComponent<Collider>();
        if (deviceCollider != null)
            deviceCollider.enabled = false;
            
        // Keep screen colliders active for interaction
        Collider[] childColliders = device.GetComponentsInChildren<Collider>();
        foreach (Collider col in childColliders)
        {
            if (col.CompareTag("PhoneScreen") || col.CompareTag("DeviceScreen"))
                col.enabled = true;
        }
        
        Debug.Log($"Picked up {deviceNames[currentDeviceIndex]} - FPS style");
        UpdateDeviceStatusUI();
    }
    
    void ReturnDevice()
    {
        if (!isHoldingDevice || currentDeviceIndex == -1) return;
        
        GameObject device = devices[currentDeviceIndex];
        
        // Return device to original position
        device.transform.SetParent(null);
        device.transform.position = originalPositions[currentDeviceIndex];
        device.transform.rotation = originalRotations[currentDeviceIndex];
        device.transform.localScale = Vector3.one;
        
        // Re-enable device's collider
        Collider deviceCollider = device.GetComponent<Collider>();
        if (deviceCollider != null)
            deviceCollider.enabled = true;
        
        Debug.Log($"Returned {deviceNames[currentDeviceIndex]}");
        
        currentDeviceIndex = -1;
        isHoldingDevice = false;
        UpdateDeviceStatusUI();
    }
    
    void UpdateDeviceStatusUI()
    {
        if (deviceStatusText != null)
        {
            if (isHoldingDevice && currentDeviceIndex != -1)
            {
                deviceStatusText.text = $"Holding: {deviceNames[currentDeviceIndex]}\n(Press R to return)";
            }
            else if (isAnimatingPickup)
            {
                deviceStatusText.text = "Picking up device...";
            }
            else
            {
                deviceStatusText.text = "Click device to pick up\n(iPhone, iPad, Laptop)";
            }
        }
    }
    
    public bool IsHoldingDevice()
    {
        return isHoldingDevice;
    }
    
    public string GetCurrentDeviceName()
    {
        if (isHoldingDevice && currentDeviceIndex != -1)
            return deviceNames[currentDeviceIndex];
        return "None";
    }
    
    public GameObject GetCurrentDevice()
    {
        if (isHoldingDevice && currentDeviceIndex != -1)
            return devices[currentDeviceIndex];
        return null;
    }
}