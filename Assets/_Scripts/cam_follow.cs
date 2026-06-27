using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    [Header("Seguimiento")]
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Zoom")]
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    [Header("Límites del mapa")]
    public Transform limiteIzquierdo;
    public Transform limiteDerecho;
    public Transform limiteSuperior;
    public Transform limiteInferior;

    private Camera cam;

    // 1 = alejando, -1 = acercando
    private int zoomDirection = 1;

    void Start()
    {
        cam = GetComponent<Camera>();
        FindPlayer();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        // Posición deseada
        Vector3 desiredPosition = target.position + offset;

        // Movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );

        // Tamaño visible de la cámara según el zoom actual
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // Aplicar límites teniendo en cuenta el zoom
        float x = Mathf.Clamp(
            smoothedPosition.x,
            limiteIzquierdo.position.x + halfWidth,
            limiteDerecho.position.x - halfWidth
        );

        float y = Mathf.Clamp(
            smoothedPosition.y,
            limiteInferior.position.y + halfHeight,
            limiteSuperior.position.y - halfHeight
        );

        transform.position = new Vector3(
            x,
            y,
            transform.position.z
        );

        HandleZoom();
    }

    void HandleZoom()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            cam.orthographicSize += zoomDirection * zoomSpeed * Time.deltaTime;

            // Llegó al máximo → comienza a devolverse
            if (cam.orthographicSize >= maxZoom)
            {
                cam.orthographicSize = maxZoom;
                zoomDirection = -1;
            }

            // Llegó al mínimo → vuelve a alejarse
            if (cam.orthographicSize <= minZoom)
            {
                cam.orthographicSize = minZoom;
                zoomDirection = 1;
            }
        }
    }

    void FindPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PhotonView pv = player.GetComponent<PhotonView>();

            if (pv != null && pv.IsMine)
            {
                target = player.transform;
                break;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (
            limiteIzquierdo == null ||
            limiteDerecho == null ||
            limiteSuperior == null ||
            limiteInferior == null
        )
            return;

        Gizmos.color = Color.yellow;

        Vector3 topLeft = new Vector3(
            limiteIzquierdo.position.x,
            limiteSuperior.position.y,
            0
        );

        Vector3 topRight = new Vector3(
            limiteDerecho.position.x,
            limiteSuperior.position.y,
            0
        );

        Vector3 bottomLeft = new Vector3(
            limiteIzquierdo.position.x,
            limiteInferior.position.y,
            0
        );

        Vector3 bottomRight = new Vector3(
            limiteDerecho.position.x,
            limiteInferior.position.y,
            0
        );

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}