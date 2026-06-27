using UnityEngine;
using TMPro;
using Photon.Pun;

public class MostrarSala : MonoBehaviour
{
    public TMP_Text textoSala;

    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            textoSala.text = PhotonNetwork.CurrentRoom.Name;
        }
        else
        {
            textoSala.text = "Conectando...";
        }
    }
}