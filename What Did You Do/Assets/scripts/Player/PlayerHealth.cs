using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [Header("Hit Effect")]
    [SerializeField] private float blinkDuration;
    private MeshRenderer meshRenderer;
    private Color defaultColor;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

    [SerializeField] private int currentHealth;
    public int maxHealth = 100;
    public Slider slider;

  

    //[SerializeField] DamageScreen damageScreen;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = currentHealth;
        slider.value = currentHealth;

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        
        //meshRenderer = GetComponentInChildren<MeshRenderer>();
        //defaultColor = meshRenderer.material.color;
    }
    public void Update()
    {
        if (overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //we wnat to fade the image so
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
       // Debug.Log("Player took damage");

        currentHealth -= damage;

        slider.value = currentHealth;

        Debug.Log("Calling PlayDamageEffect");
        //damageScreen.StartAnimation();


        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);




        //StartCoroutine(HitEffect());
        //IEnumerator HitEffect()
        //{
        //    meshRenderer.material.color = Color.white * 2f;
        //    yield return new WaitForSeconds(blinkDuration);
        //    meshRenderer.material.color = defaultColor;
        //}




        //public int currentHealth;
        //public int maxHealth;
        //public Slider slider;

        //void Start()
        //{
        //    currentHealth = maxHealth;
        //    slider.maxValue = maxHealth;
        //    slider.value = currentHealth;
        //}

        //public void TakeDamage(int amount)
        //{



        //    //currentHealth -= amount;
        //    //slider.value = currentHealth;

        //    //if (currentHealth <= 0)
        //    //{
        //    //    Destroy(gameObject);
        //    //}
        //}

    }
}

