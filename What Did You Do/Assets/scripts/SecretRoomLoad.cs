using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SecretRoomLoad : MonoBehaviour
{
    public Button antidoteButton;

    public void OnAntidoteClick()
    {

        SceneManager.LoadScene("SecretRoom");

    }

}
