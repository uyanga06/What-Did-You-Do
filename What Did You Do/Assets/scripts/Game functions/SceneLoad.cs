using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Button QuitButton;

    void Start()
    {
        QuitButton.onClick.AddListener(QuitGame);
        Cursor.lockState = CursorLockMode.None; //allows the mouse cursor to move freely and not get locked after right clicking to throw the object
    }
    public void QuitGame() // quits the game
    {
       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();





    //    Time.timeScale = 1f;
    //    Debug.Log("Quit Game");
    //    Application.Quit();
    }
}

