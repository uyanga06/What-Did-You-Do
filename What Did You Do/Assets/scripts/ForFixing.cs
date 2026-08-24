using UnityEngine;
using UnityEngine.InputSystem;

public class ForFixing : MonoBehaviour
{
    //public class WeaponAttack : MonoBehaviour
    //{
    //    //public FPController player;

    //    [SerializeField] private float weaponHitRadius;
    //    [SerializeField] private int damage = 2;

    //    [SerializeField] private LayerMask targetLayer;

    //    public Transform attackPoint;
    //    public float attackRange = 0.5f;
    //    public LayerMask enemyLayers;

    //    //public void Attack()
    //    //{
    //    //    player.Attack();
    //    //}
    //    private void Update()
    //    {


    //        DetectHit();

    //    }

    //    public void OnAttack(InputAction.CallbackContext context)
    //    {
    //        if (context.performed) //if left mouse button is pressed, invoke attack
    //        {
    //            Attack();
    //        }
    //    }

    //    void Attack()
    //    {
    //        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers); //detects enemies in range

    //        foreach (Collider enemy in hitEnemies)
    //        {
    //            Debug.Log("We hit them!");
    //        }
    //    }

    //    private void OnDrawGizmosSelected()
    //    {
    //        if (attackPoint == null) //in case attackPoint hasn't been assigned, return
    //            return;
    //        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //    }



    //    private void DetectHit()
    //    {
    //        Collider[] hit = Physics.OverlapSphere(transform.position, weaponHitRadius, targetLayer);

    //        if (hit.Length > 0)
    //        {
    //            EnemyHealth Enemy = hit[0].GetComponent<EnemyHealth>();

    //            Enemy.TakeDamage(damage);

    //            gameObject.SetActive(true);
    //        }


    //    }

    //}

}
