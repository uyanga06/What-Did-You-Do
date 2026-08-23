using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cauldron") //checks for the correct collision to then transition to the endgame scene
        {
            SceneManager.LoadScene("TransitionTest");
        }
    }
}

