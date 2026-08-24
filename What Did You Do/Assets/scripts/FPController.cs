using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// Refertnce List:
// Hayes, A. (2026). 'Week 2: First-person Character Controller'. [PowerPoint Presentation]. DIGA2001A - Digital Art Design Project, University of the Witwatersrand. Available at: https://ulwazi.wits.ac.za/courses/87753/files/11404021?module_item_id=1194555 (Accessed on 10 August 2026)
// Hayes, A. (2026). 'Week 3:  First Person Character Controller & Mechanics'. [PowerPoint Presentation]. DIGA2001A -Digital Art Design Project, University of the Witwatersrand. Available at: https://ulwazi.wits.ac.za/courses/87753/files/11447217?module_item_id=1198514 (Accessed on 10 August 2026)
public class FPController : MonoBehaviour
{
    [Header("Action Settings")] // refers to the input actions for walking, running and jumping. previously named Movement Settings
    public float moveSpeed = 50f;
    public float runSpeed = 15f;
   //private const float doubleClickTime = 0.3f; // Time window for detecting double-click for running
   // private float lastClickTime; // Time of the last click for running
    public float gravity = -60f;
    public float jumpHeight = 10f;

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

   // private Vector3 CalculateWorldDirection()
  //  {
   //     Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
   //     Vector3 worldDirection = transform.TransformDirection(inputDirection);
    //    return worldDirection.normalized;
  //  }
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

   // public void OnRun(InputAction.CallbackContext context)
   // {
   //    if(context.performed) // Handle running logic
   //    {
    //       Run(); // Set the movement speed to the running speed
   //    }
      
    //}
   // private void Run()
    //{
      //  float currentTime = Time.time;
       // if (currentTime - lastClickTime < doubleClickTime)
       // {
        //    moveSpeed = runSpeed; // Set the movement speed to the running speed
       // }
      //  else
      //  {
       //     moveSpeed = 5f; // Reset the movement speed to the walking speed
        //}
      //  lastClickTime = currentTime; // Update the last click time
  //  }

}