using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Username : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject UsernamePage;
    public TMP_Text MyUsername;

    void Start()
    {
        // ⚠️ Quita esto si quieres recordar el nombre entre sesiones
        PlayerPrefs.DeleteKey("Username");

        string savedName = PlayerPrefs.GetString("Username");

        if (string.IsNullOrEmpty(savedName))
        {
            UsernamePage.SetActive(true);
            inputField.ActivateInputField(); // 👈 foco automático
        }
        else
        {
            SetUsername(savedName);
            UsernamePage.SetActive(false);
        }
    }

    void Update()
    {
        // 🟢 Detectar ENTER
        if (UsernamePage.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            SaveUsername();
        }
    }

    public void SaveUsername()
    {
        string username = inputField.text;

        // 🟡 Si está vacío → generar InvitadoX
        if (string.IsNullOrWhiteSpace(username))
        {
            int guestNumber = PlayerPrefs.GetInt("GuestNumber", 1);
            username = "Invitado" + guestNumber;

            PlayerPrefs.SetInt("GuestNumber", guestNumber + 1);
        }

        SetUsername(username);

        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.Save();

        UsernamePage.SetActive(false);
    }

    void SetUsername(string username)
    {
        PhotonNetwork.NickName = username;
        MyUsername.text = username;
    }
}