using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Entries")]
    public DialogueEntry[] entries;

    [Header("UI References")]
    public GameObject entryCanvas;
    public GameObject dialogueBox;
    public GameObject tutorialBox;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;   
    public GameObject nextIndicator; 

    [Header("Click Receiver")]
    public CanvasClickReceiver canvasClickReceiver; // direct component reference now

    [Header("Typewriter Settings")]
    [Tooltip("Seconds between each character. Lower = faster.")]
    public float typingSpeed = 0.04f;
    public AudioSource sfxTututut;

    [Header("Settings")]
    public bool loopAtEnd = false;
    public bool hideDialogueBoxOnStart = false;
    public UnityEvent onDone;

    private int currentIndex = -1;
    private Coroutine typingCoroutine;
    public bool isTyping = false;
    private bool dialogueEnded = false;

    public float nextIndicatorAmplitude = 3f;
    public float nextIndicatorSpeed = 1f;
    private Vector3 nextIndicatorStartPosition;

    void Start()
    {
        if (hideDialogueBoxOnStart && dialogueBox != null)
            dialogueBox.SetActive(false);

        if (tutorialBox != null)
            tutorialBox.SetActive(false);

        if (entries != null && entries.Length > 0)
            ShowEntry(0);

        nextIndicatorStartPosition = nextIndicator.transform.localPosition;
    }

    void Update()
    {
        if (isTyping)
        {
            nextIndicator.SetActive(false);
        }
        else
        {
            nextIndicator.SetActive(true);
        }
        float offset = Mathf.Sin(Time.time * nextIndicatorSpeed) * nextIndicatorAmplitude;
        nextIndicator.transform.localPosition = nextIndicatorStartPosition + new Vector3(0, offset, 0);
    }


    public void Advance()
    {
        // Block advance completely once dialogue has ended
        if (dialogueEnded) return;

        if (entries == null || entries.Length == 0) return;

        if (isTyping)
        {
            SkipTyping();
            return;
        }

        int nextIndex = currentIndex + 1;

        if (nextIndex >= entries.Length)
        {
            OnDialogueEnd();
            return;
        }

        ShowEntry(nextIndex);
    }

    public void PressTutorialBox()
    {
        if (tutorialBox != null)
            tutorialBox.SetActive(false);

        if (entryCanvas != null)
            entryCanvas.SetActive(false);
    }

    public void ShowEntry(int index)
    {
        if (entries == null || index < 0 || index >= entries.Length) return;

        currentIndex = index;
        DialogueEntry entry = entries[index];

        if (dialogueBox != null)
            dialogueBox.SetActive(true);

        if (characterNameText != null)
            characterNameText.text = entry.characterName;

        if (characterImage != null)
        {
            if (entry.characterImage != null)
            {
                characterImage.sprite = entry.characterImage;
                characterImage.gameObject.SetActive(true);
            }
            else
            {
                characterImage.gameObject.SetActive(false);
            }
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(entry.dialogueText));
    }

    private IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        dialogueText.text = "";

        sfxTututut.Play();

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        sfxTututut.Stop();

        isTyping = false;
        nextIndicator.SetActive(true);
    }

    private void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = entries[currentIndex].dialogueText;
        isTyping = false;

        sfxTututut.Stop();
    }

    private void OnDialogueEnd()
    {
        Debug.Log("[DialogueManager] Reached end of dialogue.");


        dialogueEnded = true; // block any further Advance() calls

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        // Disable the click receiver so it stops forwarding taps
        if (canvasClickReceiver != null)
        {
            Debug.Log("Dialogue End!");
            canvasClickReceiver.isActive = false;
        }
            
        onDone.Invoke();
        // StartCoroutine(ShowTutorialNextFrame());
    }

    private IEnumerator ShowTutorialNextFrame()
    {
        yield return null;

        if (tutorialBox != null)
        {
            tutorialBox.SetActive(true);
            
        }
            
    }

    public int CurrentIndex => currentIndex;
    public int TotalEntries => entries != null ? entries.Length : 0;
    public bool IsLastEntry => currentIndex >= (TotalEntries - 1);
}