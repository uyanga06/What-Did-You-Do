using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackStory : MonoBehaviour
{
    //UI button that will take the player to the game scene
    public Button playButton;

    //Plays the main scene with a delay
    public void SceneDelay()
    {
        StartCoroutine(SceneLoad());
    }

    private IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("Main Scene 1");

    }
}