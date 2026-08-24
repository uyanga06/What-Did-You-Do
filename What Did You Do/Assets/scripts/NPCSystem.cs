using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Game Code Library.(2025). Add NPC and Dialogue System to your Game - Top Down Unity 2D #19.[Online Video]. Available at: https://youtu.be/eSH9mzcMRqw?si=1H-n2vGKE5wxMhHl. (Accessed on 23 August 2026)

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")] 
public class NPCSystem : ScriptableObject
{
    //dialogue data for NPC. Will be displayed and adjustable in the inspector
    public string npcName;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 2.0f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1.0f;
  
}
