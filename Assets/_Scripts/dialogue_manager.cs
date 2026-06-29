using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [Header("Referencias")]
    [SerializeField] private GameObject dialogUIObject;
    [SerializeField] private Dialog_UI dialogUI;
    
    private bool dialogRunning;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (!dialogRunning)
            StartCoroutine(DialogueRoutine(dialogue));
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
    }

    public bool IsDialogueRunning()
    {
        return dialogRunning;
    }
}