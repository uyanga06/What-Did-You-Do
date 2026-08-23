using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            myAnimationController.SetBool("doAttack", true);

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            myAnimationController.SetBool("doAttack", false);

        }


    }





}