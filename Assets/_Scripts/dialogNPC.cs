using UnityEngine;

public class dialogNPC : MonoBehaviour
{
    [Header("Diálogos")]
    [SerializeField] private DialogueData[] dialogues;

    [Header("Interacción")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [Header("Indicador de interacción")]
    [SerializeField] private GameObject interactionIcon;

    private bool playerInside;
    private int currentDialogue = 0;

    private void Start()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }

    void Update()
    {
        if (interactionIcon != null)
        {
            bool mostrar =
                playerInside &&
                (DialogManager.Instance == null ||
                !DialogManager.Instance.IsDialogueRunning());

            interactionIcon.SetActive(mostrar);
        }

        if (!playerInside)
            return;

        if (DialogManager.Instance != null &&
            DialogManager.Instance.IsDialogueRunning())
            return;

        if (Input.GetKeyDown(interactionKey))
        {
            StartCurrentDialogue();
        }
    }

    public void StartCurrentDialogue()
    {
        if (currentDialogue < dialogues.Length &&
            dialogues[currentDialogue] != null)
        {
            if (interactionIcon != null)
                interactionIcon.SetActive(false);

            DialogManager.Instance.StartDialogue(dialogues[currentDialogue]);
        }
    }

    public void NextDialogue()
    {
        if (currentDialogue < dialogues.Length - 1)
            currentDialogue++;
    }

    public void SetDialogue(int index)
    {
        if (index >= 0 && index < dialogues.Length)
            currentDialogue = index;
    }

    public int GetCurrentDialogue()
    {
        return currentDialogue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (interactionIcon != null)
                interactionIcon.SetActive(false);
        }
    }
}