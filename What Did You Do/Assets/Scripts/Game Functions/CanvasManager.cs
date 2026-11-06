using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject SettingsCanvas;

    //call this when the settings button is clicked
    public void SwitchCanvas()
    {
        if (PauseCanvas != null) PauseCanvas.SetActive(false);
        if (SettingsCanvas != null) SettingsCanvas.SetActive(true);
    }
    public void SwitchCanvas2()
    {
        if (SettingsCanvas != null) SettingsCanvas.SetActive(false);
        if (PauseCanvas != null) PauseCanvas.SetActive(true);
    }
}
