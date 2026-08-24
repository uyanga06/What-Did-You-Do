using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Button QuitButton;

    void Start()
    {
        QuitButton.onClick.AddListener(QuitGame);
        Cursor.lockState = CursorLockMode.None; //allows the mouse cursor to move freely and not get locked atfer right-clicking to throw the object 
    }

    private void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
