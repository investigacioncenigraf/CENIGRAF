using UnityEngine;

public class PulseUntilClicked : MonoBehaviour
{
    public float speed = 2f;
    public float scaleAmount = 0.1f;

    private Vector3 originalScale;
    private bool pulsing = true;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!pulsing) return;

        float scale = 1 + Mathf.Sin(Time.time * speed * Mathf.PI) * scaleAmount;
        transform.localScale = originalScale * scale;
    }

    public void StopPulse()
    {
        pulsing = false;
        transform.localScale = originalScale;
    }
}