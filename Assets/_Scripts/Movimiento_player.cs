using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;

public class MovimientoJugador : MonoBehaviourPun
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public float velocidadTurbo = 4f;

    private Animator animator;
    private Rigidbody2D rb;

    public TMP_Text nameText;

    public FloatingJoystick joystick; // joystick móvil
    public TMP_InputField chatInput;  // input del chat

    private float lastH = 0f;
    private float lastV = -1f;

    private Vector2 movimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Configuración recomendada del Rigidbody2D
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Asignar nombre del jugador (multiplayer)
        if (nameText != null)
        {
            nameText.text = photonView.Owner.NickName;
        }

        // Solo controlar el jugador local
        if (!photonView.IsMine)
        {
            enabled = false;
            return;
        }

        // Buscar joystick automáticamente si no está asignado
        if (joystick == null)
        {
            joystick = FindFirstObjectByType<FloatingJoystick>();
        }
    }

    void Update()
    {
        // Bloquear movimiento si se está escribiendo en un InputField
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            movimiento = Vector2.zero;
            return;
        }

        float movimientoX = 0f;
        float movimientoY = 0f;

        // Input desde joystick (móvil)
        if (joystick != null)
        {
            movimientoX = joystick.Horizontal;
            movimientoY = joystick.Vertical;
        }

        // Fallback a teclado (PC)
        if (movimientoX == 0 && movimientoY == 0)
        {
            movimientoX = Input.GetAxisRaw("Horizontal");
            movimientoY = Input.GetAxisRaw("Vertical");
        }

        movimiento = new Vector2(movimientoX, movimientoY).normalized;

        float speed = movimiento.magnitude;

        // Dirección para animaciones
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

        // Animaciones
        animator.SetFloat("horizontal", lastH);
        animator.SetFloat("vertical", lastV);
        animator.SetFloat("speed", speed);

        // Opcional: acelerar la animación cuando usa turbo
        animator.speed = Input.GetKey(KeyCode.T) ? 1.5f : 1f;
    }

    void FixedUpdate()
    {
        float velocidadActual = velocidad;

        // Turbo mientras se mantenga T
        if (Input.GetKey(KeyCode.T))
        {
            velocidadActual = velocidadTurbo;
        }

        // Movimiento con físicas
        rb.linearVelocity = movimiento * velocidadActual;
    }
}