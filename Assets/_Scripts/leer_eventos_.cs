using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LeerEventos : MonoBehaviour
{
    [System.Serializable]
    public class FirestoreResponse
    {
        public FirestoreDocument[] documents;
    }

    [System.Serializable]
    public class FirestoreDocument
    {
        public FirestoreFields fields;
    }

    [System.Serializable]
    public class FirestoreFields
    {
        public FirestoreString evento;
        public FirestoreString fecha;
        public FirestoreString responsable;
        public FirestoreString lugar;
    }

    [System.Serializable]
    public class FirestoreString
    {
        public string stringValue;
    }

    [System.Serializable]
    public class Evento
    {
        public string evento;
        public string fecha;
        public string responsable;
        public string lugar;
    }

    public TMP_Text texto;

    private List<Evento> eventos = new List<Evento>();
    private int indiceActual = 0;

    void Start()
    {
        StartCoroutine(CargarEventos());
    }

    IEnumerator CargarEventos()
    {
        string url =
        "https://firestore.googleapis.com/v1/projects/eventos-unity/databases/(default)/documents/eventos";

        UnityWebRequest web =
        UnityWebRequest.Get(url);

        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            texto.text =
            "Error cargando eventos";

            Debug.LogError(web.error);
            yield break;
        }

        string json =
        web.downloadHandler.text;

        Debug.Log(json);

        FirestoreResponse data =
        JsonUtility.FromJson<FirestoreResponse>(json);

        if (data == null || data.documents == null)
        {
            texto.text =
            "No se pudieron leer eventos";

            Debug.LogError("JsonUtility no pudo parsear el JSON.");
            yield break;
        }

        eventos.Clear();

        foreach (FirestoreDocument doc in data.documents)
        {
            Evento e = new Evento();

            e.evento =
            doc.fields.evento != null
            ? doc.fields.evento.stringValue
            : "";

            e.fecha =
            doc.fields.fecha != null
            ? doc.fields.fecha.stringValue
            : "";

            e.responsable =
            doc.fields.responsable != null
            ? doc.fields.responsable.stringValue
            : "";

            e.lugar =
            doc.fields.lugar != null
            ? doc.fields.lugar.stringValue
            : "";

            eventos.Add(e);

            Debug.Log(
                "Evento=" + e.evento +
                " | Fecha=" + e.fecha +
                " | Responsable=" + e.responsable +
                " | Lugar=" + e.lugar
            );
        }

        Debug.Log("Eventos encontrados: " + eventos.Count);

        StartCoroutine(MostrarEventos());
    }

    IEnumerator MostrarEventos()
    {
        while (true)
        {
            if (eventos.Count > 0)
            {
                Evento e =
                eventos[indiceActual];

                texto.text =
                    "<b>" + e.evento + "</b>\n\n" +
                    "📅 " + e.fecha + "\n" +
                    "📍 " + e.lugar + "\n" +
                    "👤 " + e.responsable;

                indiceActual++;

                if (indiceActual >= eventos.Count)
                    indiceActual = 0;
            }
            else
            {
                texto.text =
                "No hay eventos";
            }

            yield return new WaitForSeconds(10f);
        }
    }
}