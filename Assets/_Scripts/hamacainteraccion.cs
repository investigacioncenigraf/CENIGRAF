using UnityEngine;

public class HammockInteraction : MonoBehaviour
{
    public GameObject hammock;          // hamaca vacía (la escena)
    public GameObject hammockPrefab;    // prefab que aparece (sin player)

    private GameObject player;
    private GameObject currentInstance;

    private bool resting = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < 1.5f && !resting)
        {
            EnterHammock();
        }

        if (distance > 2f && resting)
        {
            ExitHammock();
        }
    }

    void EnterHammock()
    {
        resting = true;

        // ocultar player
        player.GetComponent<SpriteRenderer>().enabled = false;

        // ocultar hamaca original
        hammock.SetActive(false);

        // 🔥 instanciar prefab EXACTAMENTE en esta hamaca
        currentInstance = Instantiate(
            hammockPrefab,
            hammock.transform.position,
            hammock.transform.rotation
        );
    }

    void ExitHammock()
    {
        resting = false;

        // mostrar player
        player.GetComponent<SpriteRenderer>().enabled = true;

        // mostrar hamaca original
        hammock.SetActive(true);

        // eliminar prefab
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !resting)
        {
            player = null;
        }
    }
}