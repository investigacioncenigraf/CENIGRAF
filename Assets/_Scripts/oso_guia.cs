using UnityEngine;

public class OsoGuia : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] puntos;
    public float velocidad = 2f;
    public bool repetirRuta = false;

    [Header("Inicio")]
    public float delayInicial = 5f;

    private int puntoActual = 0;
    private Animator animator;
    private bool puedeMoverse = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (puntos.Length > 1)
        {
            // Posicionar al oso en el primer punto
            transform.position = puntos[0].position;
            puntoActual = 1;

            // Mirar hacia el siguiente punto mientras espera
            Vector2 direccionInicial =
                (puntos[1].position - puntos[0].position).normalized;

            animator.SetFloat("horizontal", direccionInicial.x);
            animator.SetFloat("vertical", direccionInicial.y);
            animator.SetFloat("speed", 0f);
        }

        Invoke(nameof(ComenzarRecorrido), delayInicial);
    }

    void ComenzarRecorrido()
    {
        puedeMoverse = true;
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            animator.SetFloat("speed", 0f);
            return;
        }

        if (puntoActual >= puntos.Length)
        {
            animator.SetFloat("speed", 0f);

            if (repetirRuta)
            {
                puntoActual = 0;
            }

            return;
        }

        Vector3 destino = puntos[puntoActual].position;

        Vector2 direccion = (destino - transform.position).normalized;

        // Actualizar animaciones
        animator.SetFloat("horizontal", direccion.x);
        animator.SetFloat("vertical", direccion.y);
        animator.SetFloat("speed", 1f);

        // Movimiento
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );

        // Llegó al punto
        if (Vector3.Distance(transform.position, destino) < 0.05f)
        {
            puntoActual++;
        }
    }
}