
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class FPController : MonoBehaviour
{
    //MUNE
    Controls playerInput;
    Controls.PlayerActions input;

    CharacterController controller;
    Animator animator;
    //AudioSource audioSource;

    bool isGrounded;

    //public const string IDLE = "Idle";
    //public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    //public const string ATTACK2 = "Attack 2";

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    //public GameObject hitEffect;
    //public AudioClip swordSwing;
    //public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    //UYANGA

    [Header("Walk Settings")] // refers to the input actions for walking. previously named Movement Settings
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Look Settings")] // refers to the input actions for looking around 
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    //private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    //U+M
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponentInChildren<Animator>();
        //audioSource = GetComponent<AudioSource>();
        
        playerInput = new Controls();
        input = playerInput.Player;
        AssignInputs();
    }

    private void Update()
    {
        HandleWalk(); //was HandleMovement
        HandleLook();

        isGrounded = controller.isGrounded;

        //SetAnimations();
    }

    //MUNE - Animations

    void AssignInputs()
    {
        input.Attack.started += ctx => Attack();
    }

    string currentAnimationState;

    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        //// PLAY THE ANIMATION //
        //currentAnimationState = newState;
        //animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    //void SetAnimations()
    //{
    //    // If player is not attacking
    //    if (!attacking)
    //    {
    //        if (velocity.x == 0 && velocity.z == 0)
    //        { ChangeAnimationState(ATTACK1); }
    //        //else
    //        //{ ChangeAnimationState(WALK); }
    //    }
    //}

    //Attaking behaviour

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        // Repeat Inputs
        if (input.Attack.IsPressed())
        { Attack(); }

        if (playerInput.Player.Attack.IsPressed())
        {
            Attack();
            Debug.Log("Attacking");
            Debug.Log("Left Button Pressed");
        }
    

    Invoke(nameof(ResetAttack), attackSpeed);
        //Invoke(nameof(AttackRaycast), attackDelay);

        //audioSource.pitch = Random.Range(0.9f, 1.1f);
       // audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }

        Debug.Log("Player attacked!");


        //else
        //{
        //    ChangeAnimationState(ATTACK2);
        //    attackCount = 0;
        //}
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    //void AttackRaycast()
    //{
    //    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, attackDistance, attackLayer))
    //    {
    //        HitTarget(hit.point);

    //        if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
    //        { T.TakeDamage(attackDamage); }
    //    }
    //}

    //void HitTarget(Vector3 pos)
    //{
    //    //audioSource.pitch = 1;
    //    //audioSource.PlayOneShot(hitSound);

    //    GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
    //    Destroy(GO, 20);
    //}



    //UYANGA
    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    //MUNE
    public void Jump()
    {
       if (playerInput.Player.Jump.IsPressed())
        {
            Jump();
        }
    }

    public void Run()
    {
        if (playerInput.Player.Jump.IsPressed())
        {
            Run();
        }
    }



    public void HandleWalk() //was HandleMovement
    {
        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -
        verticalLookLimit, verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation,
        0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}