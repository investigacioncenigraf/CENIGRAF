using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject[] personajes;
    public Transform spawnPoint;

    private bool yaSpawneado = false;

    void Update()
    {
        if (!yaSpawneado && PhotonNetwork.InRoom)
        {
            yaSpawneado = true;
            Spawn();
        }
    }

    void Spawn()
    {
        int personajeID = PlayerPrefs.GetInt("JugadorIndex", 0);

        if (personajeID >= personajes.Length)
        {
            Debug.LogError("❌ ID de personaje inválido");
            return;
        }

        GameObject personajeSeleccionado = personajes[personajeID];

        PhotonNetwork.Instantiate(
            personajeSeleccionado.name,
            spawnPoint.position,
            Quaternion.identity
        );

        Debug.Log("✅ Spawn de: " + personajeSeleccionado.name);
    }
}