using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasClickReceiver : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Drag your DialogueManager GameObject here.")]
    public DialogueManager dialogueManager;
    public AudioSource sfxClick;

    // Set to false to stop this receiver from forwarding clicks
    [ReadOnly] public bool isActive = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActive) return;
        if (dialogueManager != null)
            dialogueManager.Advance();

        sfxClick.pitch = Random.Range(0.9f, 1.2f);
        sfxClick.PlayOneShot(sfxClick.clip);
    }
}