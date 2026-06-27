using UnityEngine;
using Photon.Pun;

public class EntrarBiblioteca : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonNetwork.LoadLevel("biblioteca");
        }
    }
}