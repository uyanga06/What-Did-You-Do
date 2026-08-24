using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class FPController : MonoBehaviour
{
    [Header("Action Settings")] // refers to the input actions for walking, running and jumping. previously named Movement Settings
    public float moveSpeed = 5f;
    public float runSpeed = 12f;
    private const float doubleClickTime = 0.3f; // Time window for detecting double-click for running
    private float lastClickTime; // Time of the last click for running
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Look Settings")] // refers to the input actions for looking around 
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleWalk(); //was HandleMovement
        HandleLook();
    }
    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
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
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded) // Check that the Jump action was successfully performed and that the player is currently standing on the ground.
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Calculates the upward speed needed for the player to reach the chosen jump height while accounting for gravity.
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
       if(context.performed) // Handle running logic
       {
           Run(); // Set the movement speed to the running speed
       }
      
    }
    private void Run()
    {
        float currentTime = Time.time;
        if (currentTime - lastClickTime < doubleClickTime)
        {
            moveSpeed = runSpeed; // Set the movement speed to the running speed
        }
        else
        {
            moveSpeed = 5f; // Reset the movement speed to the walking speed
        }
        lastClickTime = currentTime; // Update the last click time
    }

    //Pick Up:
    [Header("Pick Up Settings")] //initial values for picking up an object
    public float pickupRange = 30f;
    public Transform holdPoint;
    private ItemPickUp heldObject;

    [Header("Throw Settings")] //initial values for throwing the object
    public float throwForce = 5f;
    public float throwVelocity = 1.5f;

    public void OnPickUp(InputAction.CallbackContext context) //checks if there is an object that can be picked up/dropped
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                ItemPickUp pickUp = hit.collider.GetComponent<ItemPickUp>();

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

    public void OnThrow(InputAction.CallbackContext context) //checks if there is an object that can be thrown and then calculates the throw
    {
        if (!context.performed) return;
        if (heldObject == null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * throwVelocity;

        heldObject.Throw(impulse);
        heldObject = null;

        Cursor.visible = true; //ensures that the mouse cursor is still on the screen after throwing the object
    }

}