using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Chat : MonoBehaviourPun
{
    [Header("UI")]
    public TMP_InputField inputField;
    public Transform content;
    public GameObject messagePrefab;
    public ScrollRect scrollRect; // 👈 AGREGA ESTO

    private PhotonView pv;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage();
        }
    }

    public void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
            return;

        string playerName = PhotonNetwork.NickName;
        pv.RPC("ReceiveMessage", RpcTarget.All, playerName, inputField.text);

        inputField.text = "";
    }

    [PunRPC]
    void ReceiveMessage(string sender, string message)
    {
        GameObject msg = Instantiate(messagePrefab, content);
        MessageUI msgUI = msg.GetComponent<MessageUI>();

        msgUI.SetMessage(sender, message);

        // 🔥 Auto scroll
        StartCoroutine(ScrollToBottom());
    }

    IEnumerator ScrollToBottom()
    {
        yield return null; // espera a que Unity actualice el layout
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}