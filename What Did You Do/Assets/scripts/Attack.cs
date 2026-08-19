using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animation anim;
    
    
    // CReating a script for the melee attack animation
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Debug.Log("Space key pressed");
            return;
        }
        
    }
}
