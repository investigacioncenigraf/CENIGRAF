using UnityEngine;

public class New_OsoGuia : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] puntos;
    public float velocidad = 2f;
    public bool repetirRuta = false;

    [Header("Objetos que aparecerán al finalizar el recorrido")]
    [SerializeField] private GameObject carnet;

    [Header("Referencias")]
    [SerializeField] private MisionesUI uiMisiones;

    private int puntoActual = 0;
    private Animator animator;
    private bool puedeMoverse = false;
    private bool recorridoFinalizado = false;

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

        // El carnet comienza oculto
        if (carnet != null)
            carnet.SetActive(false);

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
        // Solo iniciar el recorrido una vez
        if (recorridoFinalizado)
            return;

        // Completar la primera misión
        if (uiMisiones != null)
        {
            uiMisiones.CompletarHablarConOsoGuia();
        }

        puedeMoverse = true;
    }

    private void Update()
    {
        if (!puedeMoverse)
        {
            animator.SetFloat("speed", 0f);
            return;
        }

        // Llegó al destino
        if (puntoActual >= puntos.Length)
        {
            animator.SetFloat("speed", 0f);
            puedeMoverse = false;
            recorridoFinalizado = true;

            // Activar el segundo diálogo
            if (npcDialogue != null)
            {
                npcDialogue.NextDialogue();
            }

            // Mostrar el carnet solo una vez
            if (carnet != null && !carnet.activeSelf)
            {
                carnet.SetActive(true);
            }

            // Reiniciar recorrido si está activado
            if (repetirRuta)
            {
                recorridoFinalizado = false;
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