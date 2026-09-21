using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Controls playerInput;
    Controls.PlayerActions input;
    CharacterController controller;
    Animator animator;
    //AudioSource audioSource;

    [SerializeField] PlayerHealth ph;

    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;
    Vector3 _PlayerVelocity;
    bool isGrounded;

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;
    float xRotation = 0f;

    [Header("Pick Up")] //initial values for picking up an object
    public float pickupRange = 40f;
    public Transform holdPoint;
    private ItemPickUp heldObject;

    [Header("Throw")] //initial values for throwing the object
    public float throwForce = 5f;
    public float throwVelocity = 1.5f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //audioSource = GetComponent<AudioSource>();

        playerInput = new Controls();
        input = playerInput.Player;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        MoveInput(input.Movement.ReadValue<Vector2>()); //Takes the player's movement input
        LookInput(input.Look.ReadValue<Vector2>()); //Takes the player's camera input

        //Repeats inputs
        if (input.Attack.IsPressed())
        {
            Attack();
        }

        SetAnimations();
    }

   

    void MoveInput(Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0f, input.y); //Creates direction of movement
        move = transform.TransformDirection(move); //Player movement that follows the player's direction 
        move *= moveSpeed; //Speed of movement of the player based on moveSpeed

        //moves the player 
        controller.Move(move * Time.deltaTime);

        //keeps the player grounded
        if (controller.isGrounded && _PlayerVelocity.y < 0)
        {
            _PlayerVelocity.y = -2f;
        }

        //gravity runs every frame
        _PlayerVelocity.y += gravity * Time.deltaTime;

        //applies vertical movement for jumping or falling
        controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    void LookInput(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector2.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void AssignInputs()
    {
        input.Attack.started += ctx => Attack();
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    string currentAnimationState;

    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        // If player is not attacking
        if (!attacking)
        {
            //Gets the direction of the player
            Vector2 movement = input.Movement.ReadValue<Vector2>();

            //Checks if there is movement or not
            if (movement.sqrMagnitude < 0.01f)
            {
                ChangeAnimationState(IDLE);
            }
            else
            {
                ChangeAnimationState(WALK);
            }
        }
    }

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    //public AudioClip swordSwing;
    //public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack()
    {

        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        //Debug.Log("Attacking Enemy");

        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }

    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;

        //Debug.Log("Attack Reset");
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);

            Debug.Log(hit.collider.name);

            if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
            { T.TakeDamage(attackDamage); }
        }
    }

    void HitTarget(Vector3 pos)
    {
        //audioSource.pitch = 1;
        //audioSource.PlayOneShot(hitSound);

        //GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        //Destroy(GO, 20);

        //Debug.Log("Enemy Hit");
    }

    public void PlayerTakeDamage(int damage)
    {
        ph.PlayerTakeDamage(damage);

    }

    //Pick Up:
    public void OnPickUp() //checks if there is an object that can be picked up/dropped
    {

        //Debug.Log("OnPickUp called");

        if (heldObject == null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                ItemPickUp pickUp = hit.collider.GetComponentInParent<ItemPickUp>();

                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnThrow() //checks if there is an object that can be thrown and then calculates the throw
    {
        if (heldObject == null) return;

        Vector3 dir = cam.transform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * throwVelocity;

        heldObject.Throw(impulse);
        heldObject = null;

        Cursor.visible = true; //ensures that the mouse cursor is still on the screen after throwing the object
    }



}

/*void FixedUpdate()
   { MoveInput(input.Movement.ReadValue<Vector2>()); }

   void LateUpdate()
   { LookInput(input.Look.ReadValue<Vector2>()); }

   void MoveInput(Vector2 input)
   {
       Vector3 moveDirection = Vector3.zero;
       moveDirection.x = input.x;
       moveDirection.z = input.y;

       controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
       _PlayerVelocity.y += gravity * Time.deltaTime;
       if (isGrounded && _PlayerVelocity.y < 0)
           _PlayerVelocity.y = -2f;
       controller.Move(_PlayerVelocity * Time.deltaTime);
   }*/
