using UnityEngine;

public class TalkTest : MonoBehaviour
{
    public GameObject PopupText; 
    private BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PopupText.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PopupText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PopupText.SetActive(false);
        }
    }
}
