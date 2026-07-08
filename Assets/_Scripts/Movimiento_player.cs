using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;

public class MovimientoJugador : MonoBehaviourPun
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public float velocidadTurbo = 4f;

    [HideInInspector]
    public bool puedeMoverse = true;

    private Animator animator;
    private Rigidbody2D rb;

    public TMP_Text nameText;

    public FloatingJoystick joystick;
    public TMP_InputField chatInput;

    private float lastH = 0f;
    private float lastV = -1f;

    private Vector2 movimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        if (nameText != null)
        {
            nameText.text = photonView.Owner.NickName;
        }

        if (!photonView.IsMine)
        {
            enabled = false;
            return;
        }

        if (joystick == null)
        {
            joystick = FindFirstObjectByType<FloatingJoystick>();
        }
    }

    void Update()
    {
        // ===========================
        // BLOQUEAR MOVIMIENTO
        // ===========================
        if (!puedeMoverse)
        {
            movimiento = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("speed", 0f);
            return;
        }

        // Bloquear movimiento si escribe en el chat
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            movimiento = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("speed", 0f);
            return;
        }

        float movimientoX = 0f;
        float movimientoY = 0f;

        if (joystick != null)
        {
            movimientoX = joystick.Horizontal;
            movimientoY = joystick.Vertical;
        }

        if (movimientoX == 0 && movimientoY == 0)
        {
            movimientoX = Input.GetAxisRaw("Horizontal");
            movimientoY = Input.GetAxisRaw("Vertical");
        }

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

        float velocidadActual = velocidad;

        if (Input.GetKey(KeyCode.T))
        {
            velocidadActual = velocidadTurbo;
        }

        rb.linearVelocity = movimiento * velocidadActual;
    }
}