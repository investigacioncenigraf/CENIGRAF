using TMPro;
using UnityEngine;

public class BanderaInteractiva : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelBandera;
    [SerializeField] private TMP_Text titulo;
    [SerializeField] private TMP_Text descripcion;

    [Header("Interacción")]
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private KeyCode tecla = KeyCode.Space;

    [Header("Misiones")]
    [SerializeField] private MisionesUI uiMisiones;

    private bool jugadorCerca;
    private bool leida = false;

    private void Start()
    {
        panelBandera.SetActive(false);

        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(jugadorCerca && !panelBandera.activeSelf);
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
        titulo.text = "Bandera del SENA";

        descripcion.text =
            "La bandera del SENA representa los valores institucionales y el compromiso con la formación integral de los aprendices. " +
            "Sus colores simbolizan el crecimiento, la esperanza y el desarrollo del país.";

        panelBandera.SetActive(true);

        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        if (!leida)
        {
            leida = true;

            if (uiMisiones != null)
            {
                uiMisiones.CompletarBandera();
            }
        }
    }

    public void CerrarInformacion()
    {
        panelBandera.SetActive(false);
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

            panelBandera.SetActive(false);
        }
    }
}