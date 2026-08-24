using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Animator animator;

    public float Health;

    [SerializeField] EnemyHealth eh;

    //added
    //enum AIState
    //{
    //    chasing, Attacking
    //}


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    [SerializeField] private float attackTime = 2f; //added now
    [SerializeField] private float timeToAttack; //added now

    //Chasing added
    //[SerializeField] private float chaseRange;

    //[SerializeField] private AIState currentState;




    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //player = GameObject.Find("PlayerObj").transform;
        //agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;

        agent = GetComponent<NavMeshAgent>();

        ////added
        ////timeSinceLastSawPlayer = suspiciousTime;
        //timeToAttack = attackTime;


    }

    private void Update()
    {
        //Check for sight and attack range - of the player in terms of the enemy
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        //added
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        //if (distanceToPlayer > attackRange)
        //{
        //    currentState = AIState.Attacking;
        //    agent.velocity = Vector3.zero;
        //    agent.isStopped = true;
        //}
        //if (distanceToPlayer < attackRange)
        //{
        //    currentState = AIState.chasing;
        //    agent.isStopped = false;
        //}


    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Reached the walk point 
        if (distanceToWalkPoint.magnitude < 2f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        // random point in range calculations 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        //added now
        //if (distanceToPLayer > chaseRange)
        //{

        //}
    }

    private void AttackPlayer()
    {
        //Code to ensure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player); //to look at player as enemy approaches player

        Debug.Log("Player Attacked");

        if (!alreadyAttacked)
        {
            //Where my code for an attack should go

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;

        Debug.Log("Player attacked");
    }

    public void TakeDamage(int damage)
    {
        eh.TakeDamage(damage);
        //Health -= damage;
        Debug.Log("Player damaged");
        //if (hHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }



    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }



}


