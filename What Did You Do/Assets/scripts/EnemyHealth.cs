using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [Header("Hit Effect")]
    [SerializeField] private float blinkDuration;
    private MeshRenderer meshRenderer;
    private Color defaultColor;

    [SerializeField] private int currentHealth;
    public int maxHealth = 100;
    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = currentHealth;
        slider.value = currentHealth;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took " + damage + " damage!");

        currentHealth -= damage;

        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(HitEffect());
        IEnumerator HitEffect()
        {
            meshRenderer.material.color = Color.white * 2f;
            yield return new WaitForSeconds(blinkDuration);
            meshRenderer.material.color = defaultColor;
        }

  

        //currentHealth -= amount;
        //slider.value = currentHealth;

        //if (currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}


    }

    



}
