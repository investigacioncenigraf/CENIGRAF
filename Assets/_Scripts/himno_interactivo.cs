using System.Collections;
using UnityEngine;

public class HimnoInteractivo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelHimno;
    [SerializeField] private GameObject interactionIcon;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Interacción")]
    [SerializeField] private KeyCode tecla = KeyCode.Space;

    [Header("Misiones")]
    [SerializeField] private MisionesUI uiMisiones;

    private bool jugadorCerca;
    private bool reproduciendo;
    private bool completada;

    private void Start()
    {
        panelHimno.SetActive(false);

        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(jugadorCerca && !panelHimno.activeSelf);

        if (!jugadorCerca)
            return;

        if (Input.GetKeyDown(tecla))
        {
            AbrirPanel();
        }
    }

    private void AbrirPanel()
    {
        if (panelHimno != null)
            panelHimno.SetActive(true);

        if (!reproduciendo)
            StartCoroutine(ReproducirHimno());
    }

    IEnumerator ReproducirHimno()
    {
        reproduciendo = true;

        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        reproduciendo = false;

        if (!completada)
        {
            completada = true;

            if (uiMisiones != null)
                uiMisiones.CompletarHimno();
        }
    }

    public void CerrarPanel()
    {
        panelHimno.SetActive(false);
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

            panelHimno.SetActive(false);
        }
    }
}