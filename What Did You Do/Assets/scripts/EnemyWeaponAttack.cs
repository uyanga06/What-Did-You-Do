using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyWeaponAttack : MonoBehaviour
{
    //public FPController player;

    [SerializeField] private float weaponHitRadius;
    [SerializeField] private int damage = 2;

    [SerializeField] private LayerMask targetLayer;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public GameObject player;


    private void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);

            Debug.Log("Player weapon hit enemy!");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) //if left mouse button is pressed, invoke attack
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers); //detects enemies in range

        foreach (Collider enemy in hitPlayers)
        {
            PlayerHealth health = player.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Debug.Log("We hit them!");
        }


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) //in case attackPoint hasn't been assigned, return
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}


