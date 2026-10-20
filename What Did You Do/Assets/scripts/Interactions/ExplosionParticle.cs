using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem explodeParticle;
    [SerializeField] private Collider antidote;

    //public AudioSource audioSource;
    //private string cauldronTag = "Cauldron";

    //public AudioClip explode;

    //Changed from when right click is clicked to trigger the effect,to having it be so
    //that when an item collides with the correct collider then the effect will trigger
    private void OnCollisionEnter(Collision collision)
    {
        //If the antidote collides with the cauldron specifically, then trigger
        //the explode particle otherwise right clicking won't trigger the effect
        if (collision.collider == antidote)
        {
            Explode();
        }
    }

    public void Explode()
    {
        //Plays the explosion particle effect 
        explodeParticle.Play();
    }
}