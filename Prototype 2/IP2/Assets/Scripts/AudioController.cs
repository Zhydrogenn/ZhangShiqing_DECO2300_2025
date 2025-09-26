using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioController : MonoBehaviour
{
    [Header("Background Audio")]
    public AudioSource backgroundAudio;
    
    [Header("UI Controls")]
    public Toggle musicToggle;
    public Slider volumeSlider; // 可选：音量控制滑块
    public TextMeshProUGUI volumeLabel; // 可选：音量标签
    
    void Start()
    {
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
            // 设置初始状态
            musicToggle.isOn = backgroundAudio != null && backgroundAudio.isPlaying;
        }
        
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            if (backgroundAudio != null)
                volumeSlider.value = backgroundAudio.volume;
        }
    }
    
    void OnMusicToggleChanged(bool isOn)
    {
        if (backgroundAudio == null) return;
        
        if (isOn)
        {
            if (!backgroundAudio.isPlaying)
                backgroundAudio.Play();
        }
        else
        {
            if (backgroundAudio.isPlaying)
                backgroundAudio.Stop();
        }
        
        Debug.Log($"Background music: {(isOn ? "ON" : "OFF")}");
    }
    
    void OnVolumeChanged(float value)
    {
        if (backgroundAudio != null)
            backgroundAudio.volume = value;
            
        if (volumeLabel != null)
            volumeLabel.text = $"Volume: {value:F1}";
    }
}