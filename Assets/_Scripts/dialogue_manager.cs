using System;
using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public static event Action OnDialogueFinished;

    [Header("Referencias")]
    [SerializeField] private GameObject dialogUIObject;
    [SerializeField] private Dialog_UI dialogUI;

    private bool dialogRunning = false;

    private MovimientoJugador jugador;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (dialogUIObject != null)
            dialogUIObject.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null)
            return;

        if (!dialogRunning)
            StartCoroutine(DialogueRoutine(dialogue));
    }

    private IEnumerator DialogueRoutine(DialogueData dialogue)
    {
        dialogRunning = true;

        // Buscar el jugador local
        if (jugador == null)
        {
            MovimientoJugador[] jugadores = FindObjectsByType<MovimientoJugador>(FindObjectsSortMode.None);

            foreach (MovimientoJugador j in jugadores)
            {
                if (j.photonView.IsMine)
                {
                    jugador = j;
                    break;
                }
            }
        }

        // Bloquear movimiento
        if (jugador != null)
            jugador.puedeMoverse = false;

        dialogUIObject.SetActive(true);

        foreach (DialogueLine line in dialogue.lines)
        {
            if (line == null)
                continue;

            yield return StartCoroutine(dialogUI.ShowDialogue(line));
        }

        dialogUIObject.SetActive(false);

        // Volver a permitir movimiento
        if (jugador != null)
            jugador.puedeMoverse = true;

        dialogRunning = false;

        OnDialogueFinished?.Invoke();
    }

    public bool IsDialogueRunning()
    {
        return dialogRunning;
    }
}