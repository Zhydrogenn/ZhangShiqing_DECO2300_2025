using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightController : MonoBehaviour
{
    [Header("Office Light")]
    public Light officeLight;
    
    [Header("UI Controls")]
    public Slider lightSlider;
    public TextMeshProUGUI lightLabel;
    
    void Start()
    {
        if (lightSlider != null)
        {
            lightSlider.onValueChanged.AddListener(OnLightChanged);
            UpdateLabel(lightSlider.value);
        }
    }
    
    void OnLightChanged(float value)
    {
        if (officeLight != null)
            officeLight.intensity = value;
        UpdateLabel(value);
    }
    
    void UpdateLabel(float value)
    {
        if (lightLabel != null)
            lightLabel.text = $"Light Intensity: {value:F1}";
    }
}