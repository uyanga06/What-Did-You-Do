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
public class PopupDialogue : MonoBehaviour
{
    public NPCSystem dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    private Rigidbody rb;



    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private static GameObject currentText; //tracks which text is active

     private bool hasPlayed = false;

    public void Start()
    {
        dialoguePanel.SetActive(false);

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other) //when player enters the trigger zone, show the dialogue panel
    {
        if (hasPlayed)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            hasPlayed = true;

            if (currentText != null && currentText != dialoguePanel)
            {
                currentText.SetActive(false);
            }

            if (dialoguePanel != null) //safety scheck
            {

                dialoguePanel.SetActive(true); //show this text
                currentText = dialoguePanel; //update current text reference
                StartCoroutine(HideTextAfterDelay()); // start timer to hide it

                //Destroy(textObject, 3f);
            }


        }
    }

    public void StartDialogue()
    {
        // Time.timeScale = 0f;
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);

        dialoguePanel.SetActive(true);


        //PauseController.SetPause(true);

        StartCoroutine(TypeLIne()); //Start typing the first line of dialogue

    }

    IEnumerator TypeLIne()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            //NextLine(); //DisplayNextLine();
        }
    }

    private IEnumerator HideTextAfterDelay() //hides dialogue panel after 10 seconds
    {
        yield return new WaitForSeconds(10f);

        if (dialoguePanel != null) // safety check
        {
            dialoguePanel.SetActive(false);
        }
        if (currentText == dialoguePanel) //only hide if this is still active text
        {
            currentText = null;
        }
    }

}
