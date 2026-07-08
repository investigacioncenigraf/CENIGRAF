using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    public void Unlock()
    {
        gameObject.SetActive(false);
    }

    public void Lock()
    {
        gameObject.SetActive(true);
    }
}