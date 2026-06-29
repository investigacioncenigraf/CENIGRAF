using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog_UI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject continueArrow;

    [Header("Configuración")]
    [SerializeField]
    [Tooltip("Caracteres por segundo")]
    private float charactersPerSecond = 50f;

    private bool continuePressed;

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            continuePressed = true;
        }
    }

    //======================================================
    // Escribe una página
    //======================================================

    private IEnumerator WriteText(string text)
    {
        continuePressed = false;

        continueArrow.SetActive(false);

        dialogText.text = text;
        dialogText.maxVisibleCharacters = 0;

        float visibleCharacters = 0f;

        while (dialogText.maxVisibleCharacters < text.Length)
        {
            if (continuePressed)
            {
                dialogText.maxVisibleCharacters = text.Length;
                continuePressed = false;
                break;
            }

            visibleCharacters += charactersPerSecond * Time.deltaTime;

            dialogText.maxVisibleCharacters = Mathf.FloorToInt(visibleCharacters);

            yield return null;
        }

        dialogText.maxVisibleCharacters = text.Length;

        continueArrow.SetActive(true);
    }

    //======================================================
    // Divide en páginas
    //======================================================

    private List<string> DivideIntoPages(string fullText)
    {
        List<string> pages = new List<string>();

        string remaining = fullText.Trim();

        while (remaining.Length > 0)
        {
            dialogText.maxVisibleCharacters = 99999;
            dialogText.text = remaining;
            dialogText.ForceMeshUpdate();

            if (!dialogText.isTextOverflowing)
            {
                pages.Add(remaining);
                break;
            }

            int overflow = dialogText.firstOverflowCharacterIndex;

            int split = remaining.LastIndexOf(' ', overflow);

            if (split <= 0)
                split = overflow;

            pages.Add(remaining.Substring(0, split).TrimEnd());

            remaining = remaining.Substring(split).TrimStart();
        }

        return pages;
    }

    //======================================================
    // Mostrar diálogo completo
    //======================================================

    public IEnumerator ShowString(string text)
    {
        List<string> pages = DivideIntoPages(text);

        foreach (string page in pages)
        {
            yield return StartCoroutine(WriteText(page));

            continuePressed = false;

            yield return new WaitUntil(() => continuePressed);

            continuePressed = false;

            continueArrow.SetActive(false);
        }

        dialogText.text = "";
    }
}