using UnityEngine;
using Photon.Pun;

public class SalirLobby : MonoBehaviourPunCallbacks
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SalirAlLobby();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        SalirAlLobby();
    }
}

    void SalirAlLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby"); // nombre de tu escena lobby
    }
}