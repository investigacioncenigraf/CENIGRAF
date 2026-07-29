using UnityEngine;

public class new_MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public float velocidadTurbo = 4f;

    [HideInInspector]
    public bool puedeMoverse = true;

    private Animator animator;
    private Rigidbody2D rb;

    private float lastH = 0f;
    private float lastV = -1f;

    private Vector2 movimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            movimiento = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("speed", 0f);
            return;
        }

        float movimientoX = Input.GetAxisRaw("Horizontal");
        float movimientoY = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(movimientoX, movimientoY).normalized;

        float speed = movimiento.magnitude;

        if (speed > 0)
        {
            if (Mathf.Abs(movimientoX) > Mathf.Abs(movimientoY))
            {
                lastH = Mathf.Sign(movimientoX);
                lastV = 0;
            }
            else
            {
                lastH = 0;
                lastV = Mathf.Sign(movimientoY);
            }
        }

        animator.SetFloat("horizontal", lastH);
        animator.SetFloat("vertical", lastV);
        animator.SetFloat("speed", speed);

        animator.speed = Input.GetKey(KeyCode.T) ? 1.5f : 1f;
    }

    void FixedUpdate()
    {
        if (!puedeMoverse)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float velocidadActual = Input.GetKey(KeyCode.T) ? velocidadTurbo : velocidad;

        rb.linearVelocity = movimiento * velocidadActual;
    }
}