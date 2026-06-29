using UnityEngine;

public class dialogNPC : MonoBehaviour
{
    [SerializeField] private DialogueData dialogue;

    [SerializeField]
    private KeyCode interactionKey = KeyCode.E;

    private bool playerInside;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(interactionKey))
        {
            DialogManager.Instance.StartDialogue(dialogue);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}