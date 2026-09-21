using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [Header("Link to Material")]
    [SerializeField] private Material damageScreenMaterial;

    [Header("Animation Settings")]
    [SerializeField] private float scrollDistance = 1.0f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float maxIntensity = 1f;

    [Header("Autostart")]
    [SerializeField] private bool autoPlayOnStart = false;
    [SerializeField] private float startDelay = 0f;

    [Header("Current State")]
    [Range(0, 1)]
    [SerializeField] private float animationProgress = 0f;

    private bool isPlaying = false;
    private float startTime;
    private Vector2 startMaskOffset; //the start value of what was in the material
    private Vector2 originalMaskOffset; //The original value from the material (for reset)
    //public Material damageScreen;

    void Start()
    {
        if (damageScreenMaterial != null && damageScreenMaterial.HasProperty("MaskOffset"))
        {
            originalMaskOffset = damageScreenMaterial.GetVector("MaskOffset");
            startMaskOffset = originalMaskOffset; //This will take whta is set in the material
            Debug.Log($"Read value from material: X={startMaskOffset.x}, Y={startMaskOffset.y}");
          
        }
        else
        {
            startMaskOffset = Vector2.zero;
            originalMaskOffset = Vector2.zero;
        }
        
        if (damageScreenMaterial != null)
        {
            damageScreenMaterial.SetFloat("ScreenIntensity", 0f);
        }
        //dont need this beacsue it tells the console to play the material at the very beginning automatically
        //if (autoPlayOnStart)
        //{
        //    if (startDelay > 0)
        //        Invoke("StartAnimation", startDelay);
        //    else
        //        StartAnimation();
        //}

    }

    void Update()
    {
        if (!isPlaying || damageScreenMaterial == null) /*isPlaying && damageScreenMaterial != null*/
            return;
        
           
            float elapsed = Time.time - startTime;
            animationProgress = Mathf.Clamp01(elapsed / animationDuration);
            float intensity = Mathf.Lerp(maxIntensity, 0f, animationProgress);

            //Animate from startMaskOffset to startMaskOffset + scrollDistance no Y
            float offsetY = startMaskOffset.y + (animationProgress * scrollDistance);
            Vector2 maskOffset = new Vector2(startMaskOffset.x, offsetY);

            damageScreenMaterial.SetVector("MaskOffset", maskOffset);
            damageScreenMaterial.SetFloat("ScreenIntensity", intensity);

           
            if (animationProgress >= 1f)
            {
                isPlaying = false;
                damageScreenMaterial.SetFloat("ScreenIntensity", 0f);
                Debug.Log($"Animation complete! Final offset {maskOffset}");
            }

        if (isPlaying)
            Debug.Log($"intensity={intensity}, progress={animationProgress}");


    }

    [ContextMenu("Start animation")]

    public void StartAnimation()
    {
        if (damageScreenMaterial == null)
        {
            Debug.Log("Damage Screen Material is not assigned"); /*Material is not assigned*/
        }
        //Re-read the technical value from the material in case it was changed in runtime if (damageScreenMaterial has property "Mask offset")
        
        if (damageScreenMaterial.HasProperty("MaskOffset"))
        {
            startMaskOffset = damageScreenMaterial.GetVector("MaskOffset");
            Debug.Log($"Start offset (from material); X={startMaskOffset.x}, Y={startMaskOffset.y}");
        }

        isPlaying = true;
        startTime = Time.time;
        animationProgress = 1f;
    }

    [ContextMenu("Reset Animation")]

    public void ResetAnimation()
    {
        isPlaying = false;
        animationProgress = 0f;

        if (damageScreenMaterial != null)
            damageScreenMaterial.SetVector("MaskOffset", originalMaskOffset);
       
    }

    [ContextMenu("Show final state")]

    public void ShowFinalState()
    {
       if (damageScreenMaterial != null)
        {
            Vector2 finalOffset = new Vector2(startMaskOffset.x, startMaskOffset.y + scrollDistance);
            damageScreenMaterial.SetVector("MaskOffset", finalOffset);
            animationProgress = 1f;
        }

    }

    public void SetMaskAnimationProgress(float progress)
    {
        if (damageScreenMaterial != null)
        {
            progress = Mathf.Clamp01(progress);
            animationProgress = progress;
            Vector2 offset = new Vector2(startMaskOffset.x, startMaskOffset.y + (progress * scrollDistance));
            damageScreenMaterial.SetVector("MaskOffset", offset);
        }


    }

    public void StopAnimation()
    {
        isPlaying = false;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void PlayDamageEffect()
    {
        Debug.Log("PlayDamageEffect called");
        if (damageScreenMaterial == null)
            return;

        //startMaskOffset = originalMaskOffset;
        Debug.Log("PlayDamageEffect called");
        startTime = Time.time;
        isPlaying = true;
    }



}
