using TMPro;
using UnityEngine;

public class LogosimboloInteractivo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelLogosimbolo;
    [SerializeField] private TMP_Text titulo;
    [SerializeField] private TMP_Text descripcion;

    [Header("Interacción")]
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private KeyCode tecla = KeyCode.Space;

    [Header("Misiones")]
    [SerializeField] private MisionesUI uiMisiones;

    private bool jugadorCerca;
    private bool leido = false;

    private void Start()
    {
        panelLogosimbolo.SetActive(false);

        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(jugadorCerca && !panelLogosimbolo.activeSelf);
        }

        if (!jugadorCerca)
            return;

        if (Input.GetKeyDown(tecla))
        {
            AbrirInformacion();
        }
    }

    private void AbrirInformacion()
    {
        titulo.text = "Logosímbolo del SENA";

        descripcion.text =
            "El logosímbolo del SENA representa el camino de aprendizaje y crecimiento de cada aprendiz. " +
            "Su diseño simboliza a una persona avanzando hacia el conocimiento, el trabajo y el desarrollo personal y profesional.";

        panelLogosimbolo.SetActive(true);

        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        if (!leido)
        {
            leido = true;

            if (uiMisiones != null)
            {
                uiMisiones.CompletarLogosimbolo();
            }
        }
    }

    public void CerrarInformacion()
    {
        panelLogosimbolo.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (interactionIcon != null)
                interactionIcon.SetActive(false);

            panelLogosimbolo.SetActive(false);
        }
    }
}