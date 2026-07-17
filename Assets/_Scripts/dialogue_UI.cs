using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_UI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject continueArrow;

    [SerializeField] private Image portraitImage;

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

    //=====================================================
    // Mostrar una línea de diálogo
    //=====================================================

public IEnumerator ShowDialogue(DialogueLine line)
{
    string nombre = line.speaker;

    // Si el hablante es "Yo", mostrar el nombre del jugador
    if (nombre == "Yo")
    {
        nombre = PlayerPrefs.GetString("Username", "Jugador");
    }

    speakerName.text = nombre;

    // Mostrar retrato
    if (portraitImage != null)
    {
        portraitImage.sprite = line.portrait;
        portraitImage.enabled = line.portrait != null;
    }

    yield return StartCoroutine(ShowString(line.text));
}

    //=====================================================
    // Escribir texto
    //=====================================================

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

    //=====================================================
    // Divide el texto en páginas
    //=====================================================

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

            int splitIndex = remaining.LastIndexOf(' ', overflow);

            if (splitIndex <= 0)
                splitIndex = overflow;

            string page = remaining.Substring(0, splitIndex).TrimEnd();

            pages.Add(page);

            remaining = remaining.Substring(splitIndex).TrimStart();
        }

        return pages;
    }

    //=====================================================
    // Mostrar todas las páginas de una línea
    //=====================================================

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