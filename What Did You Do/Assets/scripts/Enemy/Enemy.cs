using NUnit.Framework.Constraints;
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

    AudioSource audioSource;
    public AudioClip zombieAttack;
    //public AudioClip zombieDefeat;

    public float Health;

    [SerializeField] EnemyHealth eh;

    //new to improve chasing 
    [SerializeField] private float chaseUpdateInterval = 0.15f;

    private float nextChaseUpdate;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    




    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        

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

        //for enemy to keep patrolling
        if (playerInSightRange)
        {
            if (playerInAttackRange)
                AttackPlayer();
            else
                ChasePlayer();
        }
        else
        {
            Patroling();
        }


        


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
        
        Debug.Log(name + " found a patrol point: " + walkPoint);
    }

    private void SearchWalkPoint()
    {
        // random point in range calculations 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, Vector3.down /*-transform.up*/, 2f, whatIsGround))
            walkPointSet = true;

        Debug.LogWarning(name + " FAILED to find a patrol point!");
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.isStopped = false;

        //added for the chasing
        if (Time.time >= nextChaseUpdate)
        {
            agent.SetDestination(player.position);
            nextChaseUpdate = Time.time + chaseUpdateInterval;
        }
    }

    private void AttackPlayer()
    {
        //Code to ensure enemy doesn't move
        // CHANGED agent.SetDestination(transform.position);
        agent.isStopped = true;
        agent.SetDestination(transform.position);

        // transform.LookAt(player); //to look at player as enemy approaches player

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;


        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360f * Time.deltaTime
            );
        }


        //if (!alreadyAttacked)
        //{
        //    //Where my code for an attack should go
        //    //Debug.Log("Player Attacked");

        //    alreadyAttacked = true;
        //    Invoke(nameof(ResetAttack), timeBetweenAttacks);

        //}
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;

        //Debug.Log("Player attacked");
    }

    public void TakeDamage(int damage)
    {
        eh.TakeDamage(damage);
        
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





    //this was under attacking at the top

    //[SerializeField] private float attackTime = 2f; //added now
    //[SerializeField] private float timeToAttack; //added now
    //Chasing added
    //[SerializeField] private float chaseRange;
    //[SerializeField] private AIState currentState;

    //this was under update 

    //added
    //float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
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

    //under awake
    //player = GameObject.Find("PlayerObj").transform;
    //agent = GetComponent<NavMeshAgent>();
    ////added
    ////timeSinceLastSawPlayer = suspiciousTime;
    //timeToAttack = attackTime;

    //at the top 
    //added
    //enum AIState
    //{
    //    chasing, Attacking
    //}

    //under chaseplayer
    //added now
    //if (distanceToPLayer > chaseRange)
    //{
    //}

    //under take damage
    //Health -= damage;
    //Debug.Log("Enemy damaged");
    //if (hHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);









}


