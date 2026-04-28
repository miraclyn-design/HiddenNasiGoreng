using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueEntry", menuName = "Dialogue/Dialogue Entry")]
public class DialogueEntry : ScriptableObject
{
    [Header("Character")]
    public string characterName;
    public Sprite characterImage;

    [Header("Dialogue")]
    [TextArea(3, 6)]
    public string dialogueText;
}