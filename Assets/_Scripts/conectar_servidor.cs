using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Conectar : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Conectando...");

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Entró al Lobby");
        SceneManager.LoadScene("Lobby");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogError("Desconectado: " + cause);
    }
}