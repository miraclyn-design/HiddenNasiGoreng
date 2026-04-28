using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialBoxClick : MonoBehaviour, IPointerClickHandler
{
    public DialogueManager dialogueManager;
    public CanvasClickReceiver canvasClickReceiver;
    public AudioSource sfxClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (canvasClickReceiver != null && canvasClickReceiver.isActive) return;
        
        Debug.Log("CLicked!");
        dialogueManager.PressTutorialBox();

        sfxClick.pitch = Random.Range(0.9f, 1.1f);
        sfxClick.Play();
    }
}