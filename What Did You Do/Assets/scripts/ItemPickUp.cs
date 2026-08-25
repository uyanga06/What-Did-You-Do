using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickUp : MonoBehaviour
{
    private Rigidbody rb;
    //public GameObject DropPrompt;
    //public GameObject ThrowPrompt;
    //public GameObject PickUpPrompt;


    /*void Start() //setup for the prompts that will be initially shown to the player 
    {
        PickUpPrompt.SetActive(true);
        DropPrompt.SetActive(false);
        ThrowPrompt.SetActive(false);
    }*/

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void PickUp(Transform holdPoint) //stops the objects physics and attaches it to the player's hand/hold point
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;

        //DropPrompt.SetActive(true);
        //ThrowPrompt.SetActive(true);
        //PickUpPrompt.SetActive(false);
    }

    public void Drop() //physics activate again, allowing for the object to be detached from the hold point 
    {
        rb.useGravity = true;
        transform.SetParent(null);

        //DropPrompt.SetActive(false);
        //ThrowPrompt.SetActive(false);
        //PickUpPrompt.SetActive(true);
    }

    public void MoveToHoldPoint(Vector3 targetPosition)
    {
        rb.MovePosition(targetPosition); //moves the object to the hold point 
    }

    public void Throw(Vector3 impulse) //physics activate again and applies a throwing force to the object
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(impulse, ForceMode.Impulse);

        //DropPrompt.SetActive(false);
        //ThrowPrompt.SetActive(false);
        //PickUpPrompt.SetActive(true);

    }
}


