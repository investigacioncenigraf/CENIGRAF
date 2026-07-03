using System;
using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    // Evento que se dispara cuando termina cualquier diálogo
    public static event Action OnDialogueFinished;

    [Header("Referencias")]
    [SerializeField] private GameObject dialogUIObject;
    [SerializeField] private Dialog_UI dialogUI;

    private bool dialogRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (dialogUIObject != null)
            dialogUIObject.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (!dialogRunning)
        {
            StartCoroutine(DialogueRoutine(dialogue));
        }
    }

    private IEnumerator DialogueRoutine(DialogueData dialogue)
    {
        dialogRunning = true;

        dialogUIObject.SetActive(true);

        foreach (DialogueLine line in dialogue.lines)
        {
            yield return StartCoroutine(dialogUI.ShowString(line.text));
        }

        dialogUIObject.SetActive(false);

        dialogRunning = false;

        // Avisar que terminó el diálogo
        OnDialogueFinished?.Invoke();
    }

    public bool IsDialogueRunning()
    {
        return dialogRunning;
    }
}