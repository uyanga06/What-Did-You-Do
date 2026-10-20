//Title: Add NPC and Dialogue System to Your Game - Top Down Unity 2D #19
//Author: Game Code Library
//Date: 23 February 2025
//Code Version: Unity 2022.3.20f1 LTS

using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDialogue : MonoBehaviour
{
    public GameObject InteractionIcon;
    //public GameObject Panel;

    public NPC currentNPC;


    public void Start()
    {
        if (InteractionIcon != null)
        {
            InteractionIcon.SetActive(false);
        }

    }

    public void Update() {
        if (currentNPC != null && Keyboard.current.eKey.wasPressedThisFrame) //checks if the player is in range of an interactable object and presses the E key
        {
            currentNPC.StartDialogue(); //starts the dialogue with the NPC
        }
    }


    private void OnTriggerEnter(Collider other) //triggers interaction icon to show when in range with an interactable object
    {
        NPC npc = other.GetComponent<NPC>(); //checks if there is an interactable script on the object
        if (npc != null)
        {
            currentNPC = npc;
            InteractionIcon.SetActive(true); //once object is in range, icon will show above player
            
        }


    }

    private void OnTriggerExit(Collider other)
    {
        NPC npc = other.GetComponent<NPC>();
        if (npc != null && npc == currentNPC) //checks if the interactble is the same one that is in range
        {
            if (InteractionIcon != null)
            {
                InteractionIcon.SetActive(false); //once object is out of range, the icon will disappear

            }

            npc.EndDialogue();

            currentNPC = null;
        }
    }

}
