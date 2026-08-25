using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //UI buttons that will be used for main menu
    public Button playButton;

    public Button exitButton;

    public void OnPlayClick()
    {

        SceneManager.LoadScene("Main Scene");

    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();


    }

    //public GameObject pic;

    //public void Trigger()
    //{
    //    if (pic.activeInHierarchy == false)
    //    {
    //        pic.SetActive(true);
    //    }
    //    else
    //    {
    //        pic.SetActive(false);
    //    }
    //}






}


