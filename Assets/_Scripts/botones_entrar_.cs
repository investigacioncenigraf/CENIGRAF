using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Ventanas")]
    [SerializeField] private GameObject ventanaOpciones;
    [SerializeField] private GameObject ventanaSalir;
    [SerializeField] private GameObject ventanaEventos;

    private void Start()
    {
        CerrarTodasLasVentanas();
    }

    #region Botones

    public void BotonEntrar()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "campus",
            new RoomOptions { MaxPlayers = 20 },
            TypedLobby.Default
        );
    }

    public void BotonOpciones()
    {
        MostrarVentana(ventanaOpciones);
    }

    public void BotonSalir()
    {
        MostrarVentana(ventanaSalir);
    }

    public void BotonEventos()
    {
        MostrarVentana(ventanaEventos);
    }

    public void CerrarVentanas()
    {
        CerrarTodasLasVentanas();
    }

    public void EntrarZajuna()
    {
        Application.OpenURL("https://zajuna.sena.edu.co/");
    }

    #endregion

    #region Ventanas

    private void MostrarVentana(GameObject ventana)
    {
        CerrarTodasLasVentanas();

        if (ventana != null)
            ventana.SetActive(true);
    }

    private void CerrarTodasLasVentanas()
    {
        if (ventanaOpciones != null)
            ventanaOpciones.SetActive(false);

        if (ventanaSalir != null)
            ventanaSalir.SetActive(false);

        if (ventanaEventos != null)
            ventanaEventos.SetActive(false);
    }

    #endregion

    #region Photon

    public override void OnJoinedRoom()
    {
        Debug.Log("Entró al Room: " + PhotonNetwork.CurrentRoom.Name);

        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(CargarEscena());
    }

    private IEnumerator CargarEscena()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Juego");
    }

    #endregion
}