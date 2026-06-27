using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public TMP_Text messageText;

    public void SetMessage(string sender, string message)
    {
        messageText.text = $"<b>{sender}:</b> {message}";

        // Hace que este mensaje sea el último (abajo en la lista)
        transform.SetAsLastSibling();
    }
}