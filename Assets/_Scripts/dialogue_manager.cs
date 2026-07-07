using System;
using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    // Evento que se dispara al terminar un diálogo
    public static event Action OnDialogueFinished;

    [Header("Referencias")]
    [SerializeField] private GameObject dialogUIObject;
    [SerializeField] private Dialog_UI dialogUI;

    private bool dialogRunning = false;

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
        {
            Debug.LogWarning("No se asignó un DialogueData.");
            return;
        }

        if (!dialogRunning)
            StartCoroutine(DialogueRoutine(dialogue));
    }

    private IEnumerator DialogueRoutine(DialogueData dialogue)
    {
        dialogRunning = true;

        dialogUIObject.SetActive(true);

        foreach (DialogueLine line in dialogue.lines)
        {
            if (line == null)
                continue;

            yield return StartCoroutine(dialogUI.ShowDialogue(line));
        }

        dialogUIObject.SetActive(false);

        dialogRunning = false;

        // Avisar a cualquier script que el diálogo terminó
        OnDialogueFinished?.Invoke();
    }

    public bool IsDialogueRunning()
    {
        return dialogRunning;
    }
}