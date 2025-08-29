using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightController : MonoBehaviour
{
    [Header("Light Settings")]
    public Slider lightSlider;
    public TextMeshProUGUI lightLabel;
    
    [Header("Environment Manager")]
    public SimpleEnvironmentManager environmentManager;
    
    void Start()
    {
        // Setup slider event
        if (lightSlider != null)
        {
            lightSlider.onValueChanged.AddListener(OnLightValueChanged);
            // Set initial value
            lightSlider.value = 1.2f;
        }
        
        // Find environment manager if not assigned
        if (environmentManager == null)
            environmentManager = FindObjectOfType<SimpleEnvironmentManager>();
            
        UpdateLightLabel();
    }
    
    public void OnLightValueChanged(float value)
    {
        if (environmentManager != null)
        {
            // Get current active light
            Light activeLight = environmentManager.isInOffice ? 
                              environmentManager.officeLight : 
                              environmentManager.cafeLight;
                              
            if (activeLight != null)
            {
                activeLight.intensity = value;
                Debug.Log($"Light intensity changed to: {value:F1}");
            }
        }
        
        UpdateLightLabel();
    }
    
    void UpdateLightLabel()
    {
        if (lightLabel != null && lightSlider != null)
        {
            lightLabel.text = $"Light Intensity: {lightSlider.value:F1}";
        }
    }
    
    // Called when environment switches to update slider for new light
    public void OnEnvironmentChanged()
    {
        if (environmentManager != null && lightSlider != null)
        {
            Light activeLight = environmentManager.isInOffice ? 
                              environmentManager.officeLight : 
                              environmentManager.cafeLight;
                              
            if (activeLight != null)
            {
                lightSlider.value = activeLight.intensity;
            }
        }
    }
}