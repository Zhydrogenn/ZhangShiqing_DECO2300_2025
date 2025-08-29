using UnityEngine;
using UnityEngine.UI;

public class SimpleDeviceManager : MonoBehaviour
{
    [Header("Figma Screens - No Login Page")]
    public Sprite[] figmaScreens; // Main page, Feature page, Settings page
    public string[] screenNames; // Corresponding screen names
    public Image screenImage;
    
    [Header("Interaction Settings")]
    public float screenClickCooldown = 0.5f;
    
    private int currentScreenIndex = 0;
    private float lastClickTime = 0f;
    
    void Start()
    {
        // Show first screen
        if (figmaScreens.Length > 0)
        {
            screenImage.sprite = figmaScreens[0];
        }
        
        // Ensure screen names array matches screens array
        if (screenNames.Length != figmaScreens.Length)
        {
            screenNames = new string[figmaScreens.Length];
            for (int i = 0; i < screenNames.Length; i++)
            {
                screenNames[i] = $"Screen {i + 1}";
            }
        }
    }
    
    public void NextScreen()
    {
        // Prevent rapid clicking
        if (Time.time - lastClickTime < screenClickCooldown)
            return;
            
        lastClickTime = Time.time;
        
        if (figmaScreens.Length == 0) return;
        
        // Switch to next screen
        currentScreenIndex = (currentScreenIndex + 1) % figmaScreens.Length;
        screenImage.sprite = figmaScreens[currentScreenIndex];
        
        // Visual feedback
        StartCoroutine(FlashScreen());
        
        Debug.Log($"Switched to: {GetCurrentScreenName()}");
    }
    
    System.Collections.IEnumerator FlashScreen()
    {
        Color original = screenImage.color;
        screenImage.color = Color.cyan; // Click feedback color
        yield return new WaitForSeconds(0.15f);
        screenImage.color = original;
    }
    
    public string GetCurrentScreenName()
    {
        if (screenNames.Length > currentScreenIndex)
            return screenNames[currentScreenIndex];
        return $"Screen {currentScreenIndex + 1}";
    }
    
    public int GetCurrentScreenIndex()
    {
        return currentScreenIndex;
    }
    
    public int GetTotalScreens()
    {
        return figmaScreens.Length;
    }
}