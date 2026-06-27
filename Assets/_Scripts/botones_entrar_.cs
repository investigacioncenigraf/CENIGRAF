using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public void EntrarCampus()
    {
        PhotonNetwork.JoinOrCreateRoom("campus",
            new RoomOptions { MaxPlayers = 20 },
            TypedLobby.Default);
    }

    public void EntrarArtes()
    {
        PhotonNetwork.JoinOrCreateRoom("artes",
            new RoomOptions { MaxPlayers = 20 },
            TypedLobby.Default);
    }
        public void EntrarABC()
    {
        PhotonNetwork.JoinOrCreateRoom("ABC contrado aprendizaje",
            new RoomOptions { MaxPlayers = 20 },
            TypedLobby.Default);
    }
            public void EntrarEventos()
    {
        PhotonNetwork.JoinOrCreateRoom("eventos",
            new RoomOptions { MaxPlayers = 20 },
            TypedLobby.Default);
    }
                
            public void EntrarZajuna()
    {
            Application.OpenURL("https://zajuna.sena.edu.co/");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entró al Room: " + PhotonNetwork.CurrentRoom.Name);

        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(CargarEscena());
    }

    IEnumerator CargarEscena()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Juego");
    }
}