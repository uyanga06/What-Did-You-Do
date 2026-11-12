using UnityEngine;

public class Mirror : MonoBehaviour
{
    //Reference: Crafting mirrors in Unity : Reflections Tutorial. (2024). [Video] Directed by UnityDude. YouTube. Available at: https://www.youtube.com/watch?v=_jBWm0KM2ec.
    
    //the cameras used to show the correct reflection/follows the player 
    public Transform PlayerCamera;
    public Transform MirrorCamera;

    void Update()
    {
        //creates a new postion 
        Vector3 positionY = new Vector3(transform.position.x, PlayerCamera.transform.position.y, transform.position.z);

        //calculates the direction from position y to the player camera
        Vector3 side1 = PlayerCamera.transform.position - positionY;

        //gets the object's forward direction
        Vector3 side2 = transform.forward;

        //calculates the angle
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        //rotates the mirror camera 
        MirrorCamera.localEulerAngles = new Vector3(0, angle, 0);
    }
}
