using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RetroPerformanceManager : MonoBehaviour
{
    [Header("Performance Mode")]
    public bool isLowPerfMode = false;

    [Header("Settings")]
    public float lowResScale = 0.4f; // 40% der Auflösung (Pixel-Look) man kann auch mehr machen
    public int animationFPS = 24;
    
    private float lastAnimUpdate;

    void Update()
    {
        // Taste zum Testen debugging halt
        if (Input.GetKeyDown(KeyCode.P))
        {
            isLowPerfMode = !isLowPerfMode;
            ApplyPerformanceMode(isLowPerfMode);
        }

        // Animationen drosseln
        if (isLowPerfMode)
        {
            UpdateAnimationStepping();
        }
    }

    void ApplyPerformanceMode(bool low)
    {
        // 1. Auflösung & Texturen niedriger
        QualitySettings.globalTextureMipmapLimit = low ? 2 : 0; // Textur-Matsch an/aus
        
      
        if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urpAsset)
        {
            urpAsset.renderScale = low ? lowResScale : 1.0f;
        }

        QualitySettings.shadowDistance = low ? 15f : 150f;
        QualitySettings.shadowCascades = low ? 0 : 2;
        QualitySettings.antiAliasing = low ? 0 : 2

        Debug.Log(low ? "Retro Mode AKTIVIERT" : "Normal Mode AKTIVIERT");
    }

    void UpdateAnimationStepping()
    {
        // Dieser Teil zwingt die Animators in nen drossel modus
        float interval = 1f / animationFPS;
        
        if (Time.time - lastAnimUpdate >= interval)
        {
            // Alle Animators in der Szene finden 
            Animator[] animators = FindObjectsByType<Animator>(FindObjectsSortMode.None);
            foreach (var anim in animators)
            {
                anim.speed = 1f; 
            }
            lastAnimUpdate = Time.time;
        }
        else
        {
            Animator[] animators = FindObjectsByType<Animator>(FindObjectsSortMode.None);
            foreach (var anim in animators)
            {
                anim.speed = 0f; // Pausieren für den ruckel look
            }
        }
    }
}
