using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    //UI buttons used on the pause screen
    public GameObject PausePanel;
    public Button PauseButton;
    public Button ResumeButton;
    public Button TitlePageButton;
    public Button SettingsButton;
    public Button QuitButton;
    //public Slider VolumeSlider;
    public GameObject HealthBars;

    public GameObject ControlsPopUp;
    private bool isPaused = false;

    void Start() //depicts the UI elements that are/are not dispalyed at the start
    {
        PausePanel.SetActive(false);
        HealthBars.SetActive(true);

        isPaused = false;
        Time.timeScale = 1f;

        //hides/locks the cursor 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //the UI buttons with their respective functions
        PauseButton.onClick.AddListener(DisplayPausePanel);
        ResumeButton.onClick.AddListener(HidePausePanel);
        QuitButton.onClick.AddListener(QuitGame);
        TitlePageButton.onClick.AddListener(LoadTitleScene);
        //SettingsButton.onClick.AddListener(LoadSettingsScreen);

        /*//the slider for the audio of the game
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = savedVolume;
        VolumeSlider.value = savedVolume;
        VolumeSlider.onValueChanged.AddListener(ChangeVolume); //handles value changes*/
    }

    void Update()
    {
        //displays the pause screen when the esc button is clicked
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                HidePausePanel();
            }
            else
            {
                DisplayPausePanel();
            }
        }
    }

    private void DisplayPausePanel()
    {
        isPaused = true;

        PausePanel.SetActive(true);
        HealthBars.SetActive(false);
        //ControlsPopUp.SetActive(false);
        Time.timeScale = 0f;

        PauseCursor();
    }

    private void HidePausePanel()
    {
        isPaused = false;

        PausePanel.SetActive(false);
        HealthBars.SetActive(true);
        //ControlsPopUp.SetActive(true);
        Time.timeScale = 1f;

        GameCursor();
    }

    private void PauseCursor() //allows it so that the cursor can be freely moved/used while the pause panel is displayed
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void GameCursor() //hides/locks the cursor when the player is in the game scene
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //what functions the UI buttons/slider depict 
    private void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void LoadTitleScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadSettingsScreen()
    {
        SceneManager.LoadScene("SettingsScreen");
    }

    /*//sets volume value based on the slider value s
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }*/
}