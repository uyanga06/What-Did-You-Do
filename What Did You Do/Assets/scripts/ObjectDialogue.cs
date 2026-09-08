//Title: Add NPC and Dialogue System to Your Game - Top Down Unity 2D #19
//Author: Game Code Library
//Date: 23 February 2025
//Code Version: Unity 2022.3.20f1 LTS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public Text dialogueText, nameText;
    public Image portraitImage;
    AudioSource source;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }


    public void Interact()
    {
        //If no dialogue data or if the game is paused and no dialogue is active
        if (dialogueData == null || ( !isDialogueActive)) //PauseController.IsGamePaused will be added later
            return;

        if (isDialogueActive) //displays next line of text
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }

    }

    public void StartDialogue()
    {
        //source = GetComponent<AudioSource>();
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.text = dialogueData.npcName;
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
       // PauseController.SetPause(true);

        StartCoroutine(TypeLine());

    }


    void NextLine()
    {

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.dialogueLines[dialogueIndex];
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {

            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();

        }
    }


    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        if (!source.isPlaying)
        {
            source.Play(); //play type sound
        }





        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed); //Dialogue text speed, types out every letter at a specific rate

        }

        isTyping = false; //Dialogue line is complete

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDisplay);
            NextLine();
        }

    }


    public void EndDialogue() //The end dialogue button to exit dialogue
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.text = "";
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (source.isPlaying)
        {
            if (source != null) // check it exists
            {
                source.Stop(); //stop typing sound
            }

        }


    }

}
