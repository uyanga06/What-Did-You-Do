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
        //player = GameObject.Find("PlayerObj").transform;
        //agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;

        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
       //Check for sight and attack range - of the player in terms of the enemy
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Reached the walk point 
        if (distanceToWalkPoint.magnitude < 1f)
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
    }

    public void TakeDamage(int damage)
    {
        eh.TakeDamage(damage);
        //Health -= damage;

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



















































































//int currentHealth;
//public int maxHealth;

//void Awake()
//{
//    currentHealth = maxHealth;
//}

//public void TakeDamage(int amount)
//{
//    currentHealth -= amount;

//    if (currentHealth <= 0)
//    { Death(); }
//}

//void Death()
//{
//    // Death function
//    // TEMPORARY: Destroy Object
//    Destroy(gameObject);
//}

//[Header("References")]
//public Transform player;
//public Animator animator;
//public PlayerHealth playerHealth;

//[Header("Settings")]
//public float detectionRadius = 15f;
//public float attackRange = 2f;
//public float patrolRadius = 20f;
//public float attackCooldown = 2f;
//public float patrolIdleTime = 3f;
//public float rotationSpeed = 7f;
////public float attackDuration = 1.0f; // Duration of attack animation 

//private NavMeshAgent agent;
//private float cooldownTimer;
//private float idleTimer;
//private float attackTimer;

//private Vector3 patrolPoint;
//private bool isPatrolling;
//private bool isIdle;
//private bool isAttacking;

//private enum State { Patrol, Chase, Attack }
//private State currentState;

//void Start()
//{
//    agent = GetComponent<NavMeshAgent>();
//    if (animator == null) animator = GetComponent<Animator>();
//    if (playerHealth == null && player != null) playerHealth = player.GetComponent<PlayerHealth>();

//    SetNewPatrolPoint();
//    currentState = State.Patrol;
//}

//void Update()
//{
//    cooldownTimer -= Time.deltaTime;

//    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

//    // Cancel attack if player leaves attack range
//    if (isAttacking && distanceToPlayer > attackRange)
//    {
//        CancelAttack();
//        currentState = State.Chase;
//    }

//    // Handle attack duration manually (no animation event needed)
//    if (isAttacking)
//    {
//        attackTimer -= Time.deltaTime;
//        if (attackTimer <= 0f)
//        {
//            EndAttack();
//        }
//    }

//    // State switching
//    if (!isAttacking)
//    {
//        if (distanceToPlayer <= attackRange && cooldownTimer <= 0f)
//            currentState = State.Attack;
//        else if (distanceToPlayer <= detectionRadius)
//            currentState = State.Chase;
//        else
//            currentState = State.Patrol;
//    }

//    // Execute state
//    switch (currentState)
//    {
//        case State.Patrol: Patrol(); break;
//        case State.Chase: ChasePlayer(); break;
//        case State.Attack: Attack(); break;
//    }

//    animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f && !isAttacking);

//    if (!isAttacking)
//        RotateTowardsMovementDirection();
//}

//void Patrol()
//{
//    if (isIdle)
//    {
//        idleTimer += Time.deltaTime;
//        if (idleTimer >= patrolIdleTime)
//        {
//            SetNewPatrolPoint();
//            idleTimer = 0f;
//        }
//        return;
//    }

//    if (!isPatrolling || Vector3.Distance(transform.position, patrolPoint) < 1.5f)
//    {
//        isIdle = true;
//        isPatrolling = false;
//        agent.ResetPath();
//    }
//}

//void SetNewPatrolPoint()
//{
//    Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + transform.position;

//    if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
//    {
//        patrolPoint = hit.position;
//        agent.SetDestination(patrolPoint);
//        isPatrolling = true;
//        isIdle = false;
//    }
//}

//void ChasePlayer()
//{
//    isIdle = false;
//    isPatrolling = false;

//    if (agent.isOnNavMesh && player != null)
//        agent.SetDestination(player.position);
//}

//void Attack()
//{
//    if (isAttacking) return;

//    float distance = Vector3.Distance(transform.position, player.position);
//    if (distance > attackRange)
//    {
//        currentState = State.Chase;
//        return;
//    }

//    isAttacking = true;
//    cooldownTimer = attackCooldown;
//    //attackTimer = attackDuration;
//    agent.ResetPath();

//    // Rotate to face player instantly
//    Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
//    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position), Time.deltaTime * rotationSpeed);

//    animator.ResetTrigger("EnemyAttack");
//    animator.SetTrigger("EnemyAttack");
//}

//public void DealDamage()
//{
//    if (Vector3.Distance(transform.position, player.position) <= attackRange)
//    {
//        playerHealth.TakeDamage(10); // Damage amount
//    }
//}

//public void EndAttack()
//{
//    isAttacking = false;
//    attackTimer = 0f;
//}

//public void CancelAttack()
//{
//    if (!isAttacking) return;

//    isAttacking = false;
//    attackTimer = 0f;
//    cooldownTimer = attackCooldown;

//    animator.ResetTrigger("EnemyAttac1k");

//    // Instantly cut the attack animation
//    if (animator.HasState(0, Animator.StringToHash("Walk")))
//        animator.CrossFade("Walk", 0.1f);
//    else if (animator.HasState(0, Animator.StringToHash("Walk")))
//        animator.CrossFade("Walk", 0.1f);

//    if (agent.isOnNavMesh && player != null)
//        agent.SetDestination(player.position);
//}

//void RotateTowardsMovementDirection()
//{
//    if (agent.velocity.sqrMagnitude > 0.1f)
//    {
//        Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
//    }
//}