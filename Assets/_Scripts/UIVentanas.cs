using UnityEngine;

public class UIVentanas : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelMisiones;
    [SerializeField] private GameObject panelOpciones;

    private void Start()
    {
        // Al iniciar ambos paneles permanecen ocultos
        panelMisiones.SetActive(false);
        panelOpciones.SetActive(false);
    }

    //=========================
    // BOTÓN MISIONES
    //=========================

    public void AbrirMisiones()
    {
        panelOpciones.SetActive(false);
        panelMisiones.SetActive(true);
    }

    public void CerrarMisiones()
    {
        panelMisiones.SetActive(false);
    }

    //=========================
    // BOTÓN OPCIONES
    //=========================

    public void AbrirOpciones()
    {
        panelMisiones.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
    }
}