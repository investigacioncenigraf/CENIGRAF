using System.Collections;
using UnityEngine;

public class UIVentanas : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private RectTransform panelMisiones;
    [SerializeField] private RectTransform panelOpciones;

    [Header("Animación")]
    [SerializeField] private float duracion = 0.3f;
    [SerializeField] private float distancia = 700f;

    private Vector2 posMisiones;
    private Vector2 posOpciones;

    private void Start()
    {
        posMisiones = panelMisiones.anchoredPosition;
        posOpciones = panelOpciones.anchoredPosition;

        panelMisiones.gameObject.SetActive(false);
        panelOpciones.gameObject.SetActive(false);
    }

    //=========================
    // MISIONES
    //=========================

    public void AbrirMisiones()
    {
        panelOpciones.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(Abrir(panelMisiones, posMisiones));
    }

    public void CerrarMisiones()
    {
        StopAllCoroutines();
        StartCoroutine(Cerrar(panelMisiones, posMisiones));
    }

    //=========================
    // OPCIONES
    //=========================

    public void AbrirOpciones()
    {
        panelMisiones.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(Abrir(panelOpciones, posOpciones));
    }

    public void CerrarOpciones()
    {
        StopAllCoroutines();
        StartCoroutine(Cerrar(panelOpciones, posOpciones));
    }

    //=========================
    // ANIMACIONES
    //=========================

    IEnumerator Abrir(RectTransform panel, Vector2 posicionFinal)
    {
        panel.gameObject.SetActive(true);

        Vector2 inicio = posicionFinal - new Vector2(0, distancia);
        panel.anchoredPosition = inicio;

        float t = 0;

        while (t < duracion)
        {
            t += Time.deltaTime;
            panel.anchoredPosition = Vector2.Lerp(inicio, posicionFinal, t / duracion);
            yield return null;
        }

        panel.anchoredPosition = posicionFinal;
    }

    IEnumerator Cerrar(RectTransform panel, Vector2 posicionFinal)
    {
        Vector2 fin = posicionFinal - new Vector2(0, distancia);

        float t = 0;

        while (t < duracion)
        {
            t += Time.deltaTime;
            panel.anchoredPosition = Vector2.Lerp(posicionFinal, fin, t / duracion);
            yield return null;
        }

        panel.gameObject.SetActive(false);
        panel.anchoredPosition = posicionFinal;
    }
}