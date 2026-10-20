using System.Collections;
using UnityEngine;
using static System.Net.WebRequestMethods;
using static UnityEngine.Rendering.DebugUI.Table;

public class AudioManager : MonoBehaviour
{
    //Reference: 
    //How to add footsteps sounds in unity 6. (2025). [Video] Directed by Unity Unlocked.YouTube.Available at: https://www.youtube.com/watch?v=mXjCJM5q4Vo.
    public static AudioManager instance; //shared reference so other scripts can access the AudioManager 

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip audioClip, float volume = 3f)
    {
        StartCoroutine(PlaySoundCoroutine(audioClip, volume)); //starts a coroutine to play the sound 
    }

    IEnumerator PlaySoundCoroutine(AudioClip audioClip, float volume = 1f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>(); //adds an AudioSource component to the AudioSource game object 
        audioSource.clip = audioClip; //plays the assigned the audio clip 
        audioSource.volume = volume; 
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length * 2); //waits till the audio is finished 

        Destroy(audioSource); //destroys the clip when it is done playing
    }
}
