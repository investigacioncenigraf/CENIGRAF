using UnityEngine;

public class TriggerParticles : MonoBehaviour
{
    public ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (particles != null)
            {
                particles.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (particles != null)
            {
                particles.Stop();
            }
        }
    }
}