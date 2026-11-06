using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SettingsScreen : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject PausePanel;
    public GameObject HealthBars;
    public Button backButton;
    public Slider VolumeSlider;

    private bool isSet = false;

    void Start()
    {
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);
        HealthBars.SetActive(false);

        backButton.onClick.AddListener(ReturnToPause);

        //the slider for the audio of the game
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = savedVolume;
        VolumeSlider.value = savedVolume;
        VolumeSlider.onValueChanged.AddListener(ChangeVolume); //handles value changes 
    }

    //when clicking the back button it takes the player back to the pause screen 
    private void ReturnToPause()
    {
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);

        HealthBars.SetActive(false);
        Time.timeScale = 0f;

        isSet = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //sets volume value based on the slider value s
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }

}
