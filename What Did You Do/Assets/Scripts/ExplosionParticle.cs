using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem explodeParticle;

    private void Update()
    {
        //Checks if the right mouse button was clicked
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
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