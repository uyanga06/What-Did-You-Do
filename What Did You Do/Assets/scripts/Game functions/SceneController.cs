using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    //UI button to transition from main scene to secret room/witch's lab
    //public void ChangeScene(string SecretRoom)
    //{
    //    SceneManager.LoadScene("SecretRoom");
    //}
    public Button antidoteButton;


    public void OnAntidoteClick()
    {
        SceneManager.LoadScene("SecretRoom");

       // Debug.Log("Scene change");
    }
}
