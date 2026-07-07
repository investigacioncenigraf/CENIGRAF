using UnityEngine;

public class OsoGuia : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] puntos;
    public float velocidad = 2f;
    public bool repetirRuta = false;

    private int puntoActual = 0;
    private Animator animator;
    private bool puedeMoverse = false;

    // Referencia al script de diálogo del NPC
    private dialogNPC npcDialogue;

    private void OnEnable()
    {
        DialogManager.OnDialogueFinished += IniciarRecorrido;
    }

    private void OnDisable()
    {
        DialogManager.OnDialogueFinished -= IniciarRecorrido;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        npcDialogue = GetComponent<dialogNPC>();

        if (puntos.Length > 1)
        {
            transform.position = puntos[0].position;
            puntoActual = 1;

            Vector2 direccionInicial =
                (puntos[1].position - puntos[0].position).normalized;

            animator.SetFloat("horizontal", direccionInicial.x);
            animator.SetFloat("vertical", direccionInicial.y);
            animator.SetFloat("speed", 0f);
        }
    }

    public void IniciarRecorrido()
    {
        puedeMoverse = true;
    }

    private void Update()
    {
        if (!puedeMoverse)
        {
            animator.SetFloat("speed", 0f);
            return;
        }

        if (puntoActual >= puntos.Length)
        {
            animator.SetFloat("speed", 0f);

            // Detener el movimiento
            puedeMoverse = false;

            // Cambiar al siguiente diálogo del NPC
            if (npcDialogue != null)
            {
                npcDialogue.NextDialogue();
            }

            // Si la ruta es repetible, reiniciarla
            if (repetirRuta)
            {
                puntoActual = 0;
            }

            return;
        }

        Vector3 destino = puntos[puntoActual].position;

        Vector2 direccion = (destino - transform.position).normalized;

        animator.SetFloat("horizontal", direccion.x);
        animator.SetFloat("vertical", direccion.y);
        animator.SetFloat("speed", 1f);

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destino) < 0.05f)
        {
            puntoActual++;
        }
    }
}