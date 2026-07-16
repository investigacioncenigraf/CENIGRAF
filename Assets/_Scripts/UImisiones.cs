using TMPro;
using UnityEngine;

public class MisionesUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text textoMisiones;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoMisionCompletada;

    // Progreso de las misiones
    private int progresoHablarConOsoGuia = 0;
    private int progresoCarnet = 0;
    private int progresoBandera = 0;
    private int progresoLogosimbolo = 0;
    private int progresoHimno = 0;

    private void Start()
    {
        ActualizarMisiones();
    }

    //===========================
    // MÉTODOS PARA COMPLETAR MISIONES
    //===========================

    public void CompletarHablarConOsoGuia()
    {
        if (progresoHablarConOsoGuia == 1)
            return;

        progresoHablarConOsoGuia = 1;
        ActualizarMisiones();
        ReproducirSonidoMision();
    }

    public void CompletarCarnet()
    {
        if (progresoCarnet == 1)
            return;

        progresoCarnet = 1;
        ActualizarMisiones();
        ReproducirSonidoMision();
    }

    public void CompletarBandera()
    {
        if (progresoBandera == 1)
            return;

        progresoBandera = 1;
        ActualizarMisiones();
        ReproducirSonidoMision();
    }

    public void CompletarLogosimbolo()
    {
        if (progresoLogosimbolo == 1)
            return;

        progresoLogosimbolo = 1;
        ActualizarMisiones();
        ReproducirSonidoMision();
    }

    public void CompletarHimno()
    {
        if (progresoHimno == 1)
            return;

        progresoHimno = 1;
        ActualizarMisiones();
        ReproducirSonidoMision();
    }

    //===========================
    // ACTUALIZAR TEXTO
    //===========================

    private void ActualizarMisiones()
    {
        textoMisiones.text =
            FormatearMision(1, "Hablar con el Oso Guía", progresoHablarConOsoGuia) +
            "\n\n" +
            FormatearMision(2, "Conseguir tu carnet SENA", progresoCarnet) +
            "\n\n" +
            FormatearMision(3, "Identificar la bandera del SENA", progresoBandera) +
            "\n\n" +
            FormatearMision(4, "Encontrar el logosímbolo del SENA", progresoLogosimbolo) +
            "\n\n" +
            FormatearMision(5, "Escuchar el himno del SENA", progresoHimno);
    }

    private string FormatearMision(int numero, string nombre, int progreso)
    {
        if (progreso >= 1)
        {
            return $"<s>{numero}. {nombre} (1/1)</s>";
        }

        return $"{numero}. {nombre} (0/1)";
    }

    //===========================
    // SONIDO
    //===========================

    private void ReproducirSonidoMision()
    {
        if (audioSource != null && sonidoMisionCompletada != null)
        {
            audioSource.PlayOneShot(sonidoMisionCompletada);
        }
    }
}