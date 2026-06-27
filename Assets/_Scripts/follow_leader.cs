using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public float distance = 2f;
    public float speed = 5f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        float magnitude = dir.magnitude;

        // Movimiento
        if (magnitude > distance)
        {
            Vector3 moveDir = dir.normalized;

            transform.position += moveDir * speed * Time.deltaTime;

            // Rotación
            transform.forward = moveDir;

            // Animator
            anim.SetFloat("horizontal", moveDir.x);
            anim.SetFloat("vertical", moveDir.z);
            anim.SetFloat("speed", 1f);
        }
        else
        {
            // Quieto
            anim.SetFloat("horizontal", 0);
            anim.SetFloat("vertical", 0);
            anim.SetFloat("speed", 0);
        }
    }
}