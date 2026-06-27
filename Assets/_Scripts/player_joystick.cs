using UnityEngine;
using Photon.Pun;

public class Player_joys : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;

    private PhotonView pv;

    void Awake()
    {
        pv = GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            joystick = FindAnyObjectByType<Joystick>();
        }
    }

    void Update()
    {
        if (!pv.IsMine) return;

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        Vector3 direction = new Vector3(h, 0f, v);

        transform.position += direction * speed * Time.deltaTime;
    }
}