using UnityEngine;
using UnityEngine.UI;
using TMPro; // 支持TextMeshPro

public class SimpleEnvironmentManager : MonoBehaviour
{
    [Header("Environment Settings")]
    public GameObject officeEnvironment;
    public GameObject cafeEnvironment;
    public Light officeLight;
    public Light cafeLight;
    
    [Header("UI Elements")]
    public Button environmentSwitchButton;
    public TextMeshProUGUI buttonText; // 改为TextMeshProUGUI
    public TextMeshProUGUI statusText; // 改为TextMeshProUGUI
    
    [Header("Light Controller")]
    public LightController lightController; // 新增光照控制器引用
    
    [Header("Test State")]
    public bool isInOffice = true;
    
    void Start()
    {
        SetOfficeEnvironment();
        
        if (environmentSwitchButton != null)
            environmentSwitchButton.onClick.AddListener(SwitchEnvironment);
            
        // Find light controller if not assigned
        if (lightController == null)
            lightController = FindObjectOfType<LightController>();
    }
    
    public void SwitchEnvironment()
    {
        if (isInOffice)
        {
            SetCafeEnvironment();
        }
        else
        {
            SetOfficeEnvironment();
        }
        
        // Notify light controller about environment change
        if (lightController != null)
            lightController.OnEnvironmentChanged();
    }
    
    void SetOfficeEnvironment()
    {
        officeEnvironment.SetActive(true);
        cafeEnvironment.SetActive(false);
        
        if (officeLight != null) officeLight.enabled = true;
        if (cafeLight != null) cafeLight.enabled = false;
            
        isInOffice = true;
        
        if (buttonText != null) buttonText.text = "Switch to Cafe";
        if (statusText != null) statusText.text = "Environment: Office";
            
        Debug.Log("Switched to Office Environment");
    }
    
    void SetCafeEnvironment()
    {
        officeEnvironment.SetActive(false);
        cafeEnvironment.SetActive(true);
        
        if (officeLight != null) officeLight.enabled = false;
        if (cafeLight != null) cafeLight.enabled = true;
            
        isInOffice = false;
        
        if (buttonText != null) buttonText.text = "Switch to Office";
        if (statusText != null) statusText.text = "Environment: Cafe";
            
        Debug.Log("Switched to Cafe Environment");
    }
    
    public string GetCurrentEnvironmentName()
    {
        return isInOffice ? "Office" : "Cafe";
    }
}