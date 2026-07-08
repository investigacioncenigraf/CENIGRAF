using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;

    [Header("NPC")]
    [SerializeField] private dialogNPC npc;

    [SerializeField]
    private int nextDialogueIndex = 2;

    [Header("Referencias")]
    [SerializeField] private MisionesUI uiMisiones;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        // Completar la misión "Conseguir tu carnet SENA"
        if (uiMisiones != null)
        {
            uiMisiones.CompletarCarnet();
        }

        // Sonido
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                Camera.main.transform.position);
        }

        // Desbloquear todas las zonas restringidas
        RestrictedArea[] areas = FindObjectsByType<RestrictedArea>(FindObjectsSortMode.None);

        foreach (RestrictedArea area in areas)
        {
            area.Unlock();
        }

        // Cambiar al diálogo 3
        if (npc != null)
        {
            npc.SetDialogue(nextDialogueIndex);
            npc.StartCurrentDialogue();
        }

        // Desaparece el carnet
        gameObject.SetActive(false);
    }
}