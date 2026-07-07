using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;

    public Sprite portrait;

    [TextArea(3,8)]
    public string text;
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;
}