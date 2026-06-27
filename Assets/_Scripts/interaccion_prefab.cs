using UnityEngine;

public class interaccion_prefab : MonoBehaviour

{
    [Header("Referencias")]
    public GameObject baseObject;       // objeto original (opcional)
    public GameObject interactionPrefab; // prefab que aparece

    [Header("Configuración")]
    public float enterDistance = 1.5f;
    public float exitDistance = 2f;

    public bool hidePlayerSprite = true;
    public bool disablePlayerMovement = false;

    private GameObject player;
    private GameObject currentInstance;

    private bool isActive = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < enterDistance && !isActive)
        {
            EnterInteraction();
        }

        if (distance > exitDistance && isActive)
        {
            ExitInteraction();
        }
    }

    void EnterInteraction()
    {
        isActive = true;

        // ocultar player visualmente
        if (hidePlayerSprite)
        {
            var sr = player.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;
        }

        // desactivar movimiento (si quieres)
        if (disablePlayerMovement)
        {
            var movement = player.GetComponent<MovimientoJugador>(); 
            if (movement != null) movement.enabled = false;
        }

        // ocultar objeto base
        if (baseObject != null)
        {
            baseObject.SetActive(false);
        }

        // instanciar prefab en esta posición
        currentInstance = Instantiate(
            interactionPrefab,
            transform.position,
            transform.rotation
        );
    }
    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Algo entró: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("PLAYER DETECTADO");
        player = other.gameObject;
    }
}

    void ExitInteraction()
    {
        isActive = false;

        // mostrar player
        if (hidePlayerSprite)
        {
            var sr = player.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;
        }

        // activar movimiento
        if (disablePlayerMovement)
        {
            var movement = player.GetComponent<MonoBehaviour>(); // 👈 cambia por tu script real
            if (movement != null) movement.enabled = true;
        }

        // mostrar objeto base
        if (baseObject != null)
        {
            baseObject.SetActive(true);
        }

        // destruir prefab
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            player = null;
        }
    }
}