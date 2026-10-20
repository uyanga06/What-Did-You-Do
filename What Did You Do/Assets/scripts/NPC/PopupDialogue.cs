using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
// Reference List:
// Game Code Library.(2025). 'Add NPC and Dialogue System to your Game - Top Down Unity 2D #19'.[Online Video]. Available at: https://youtu.be/eSH9mzcMRqw?si=1H-n2vGKE5wxMhHl. (Accessed on 23 August 2026)
// KaiJPR.(2023). 'Level Up Your Game: Creating NPC Dialogue System - Unity Tutorial #1'.[Online Video]. Available at: https://youtu.be/X-7A0WzSx5A?si=TWrzuQj9gWXglW08 (Accessed on 23 August 2026)
public class PopupDialogue : MonoBehaviour, IInteractable
{
    public NPCSystem dialogueData;
    public GameObject DialoguePanel;
    public TMP_Text DialogueText, NameText  ;
    private Rigidbody rb;


    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private static GameObject currentText; //tracks which text is active

    // private bool hasPlayed = false;

    public void Start()
    {
        DialoguePanel.SetActive(false);

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other) //when player enters the trigger zone, show the dialogue panel
    {
        //    if (hasPlayed)
        //    {
        //        return;
        //    }
        //   if (other.CompareTag("Player"))
        //    {
        //        hasPlayed = true;

        //        if (currentText != null && currentText != dialoguePanel)
        //        {
        //            currentText.SetActive(false);
        //        }

        //        if (dialoguePanel != null) //safety scheck
        //        {
        //          InteractionIcon.SetActive(true); //show the interaction icon
        //           // dialoguePanel.SetActive(true); //show this text
        //           // currentText = dialoguePanel; //update current text reference
        //           // StartCoroutine(HideTextAfterDelay()); // start timer to hide it

        //            // Destroy(textObject, 3f);
        //        }

        //   }
    }






    public bool CanInteract()
    {
        return !isDialogueActive; //dialogue can only be initiated if it is not already active
    }

    public void Interact()
    {

        //if there is no dialogue data or the dialogue is inactive, return
        if ((dialogueData == null || !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine(); //Next line of dialogue
        }
        else
        {
            StartDialogue();
        }

    }

    public void StartDialogue()
    {
        // Time.timeScale = 0f;
        isDialogueActive = true;
        dialogueIndex = 0;

        NameText.SetText(dialogueData.npcName);

        DialoguePanel.SetActive(true);


        //PauseController.SetPause(true);

        StartCoroutine(TypeLIne()); //Start typing the first line of dialogue

    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            DialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else
        {
            dialogueIndex++;
            if (dialogueIndex < dialogueData.dialogueLines.Length)
            {
                StartCoroutine(TypeLIne());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    IEnumerator TypeLIne()
    {
        isTyping = true;
        DialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine(); //DisplayNextLine();
        }


    }
    public void EndDialogue()
    {
        // Time.timeScale = 1f;
        StopAllCoroutines();
        isDialogueActive = false;
        DialogueText.SetText("");
        DialoguePanel.SetActive(false);
        //PauseController.SetPause(false);


    }
    private IEnumerator HideTextAfterDelay() //hides dialogue panel after 10 seconds
    {
        yield return new WaitForSeconds(10f);

        if (DialoguePanel != null) // safety check
        {
            DialoguePanel.SetActive(false);
        }
        if (currentText == DialoguePanel) //only hide if this is still active text
        {
            currentText = null;
        }
    }

}
