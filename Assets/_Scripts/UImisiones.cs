using TMPro;
using UnityEngine;

public class MisionesUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text textoMisiones;

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
        progresoHablarConOsoGuia = 1;
        ActualizarMisiones();
    }

    public void CompletarCarnet()
    {
        progresoCarnet = 1;
        ActualizarMisiones();
    }

    public void CompletarBandera()
    {
        progresoBandera = 1;
        ActualizarMisiones();
    }

    public void CompletarLogosimbolo()
    {
        progresoLogosimbolo = 1;
        ActualizarMisiones();
    }

    public void CompletarHimno()
    {
        progresoHimno = 1;
        ActualizarMisiones();
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
            FormatearMision(3, "Visitar la bandera del SENA", progresoBandera) +
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
}