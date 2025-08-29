using UnityEngine;
using UnityEngine.UI;
using TMPro; // 支持TextMeshPro

public class InteractionManager : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 5f;
    public Camera playerCamera;
    
    [Header("UI Feedback")]
    public Image crossHair;
    public TextMeshProUGUI instructionText; // 改为TextMeshProUGUI
    public Color normalColor = Color.white;
    public Color interactableColor = Color.yellow;
    
    [Header("Device Manager")]
    public SimpleDeviceManager deviceManager;
    
    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
            
        if (deviceManager == null)
            deviceManager = FindObjectOfType<SimpleDeviceManager>();
            
        // Show instructions
        if (instructionText != null)
        {
            instructionText.text = "Controls:\n" +
                                 "WASD - Move\n" +
                                 "Mouse - Look around\n" +
                                 "Right Click - Look around\n" +
                                 "Left Click - Interact\n" +
                                 "ESC - Toggle cursor lock";
        }
    }
    
    void Update()
    {
        CheckInteractable();
        HandleInteraction();
        HandleCursorToggle();
    }
    
    void CheckInteractable()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.CompareTag("PhoneScreen"))
            {
                if (crossHair != null)
                    crossHair.color = interactableColor;
            }
            else
            {
                if (crossHair != null)
                    crossHair.color = normalColor;
            }
        }
        else
        {
            if (crossHair != null)
                crossHair.color = normalColor;
        }
    }
    
    void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                // Check if holding a device first
                DevicePickupController pickupController = FindObjectOfType<DevicePickupController>();
                
                // If holding device and clicking screen, interact with device
                if (pickupController != null && pickupController.IsHoldingDevice() && 
                    (hit.collider.CompareTag("PhoneScreen") || hit.collider.CompareTag("DeviceScreen")))
                {
                    Debug.Log("Interacted with held device screen!");
                    
                    // Call device manager to switch screen
                    if (deviceManager != null)
                        deviceManager.NextScreen();
                }
                // If not holding device and clicking device screen on table
                else if (!pickupController.IsHoldingDevice() && hit.collider.CompareTag("PhoneScreen"))
                {
                    Debug.Log("Clicked phone screen on table!");
                    
                    if (deviceManager != null)
                        deviceManager.NextScreen();
                }
            }
        }
    }
    
    void HandleCursorToggle()
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