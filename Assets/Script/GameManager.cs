using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalItemAmount = 5;
    public int currentItemAmount = 0;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] ParticleSystem WinParticle;
    public AudioSource sfxParticle;
    public AudioSource sfxYey;

    public TMP_Text txtItem;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else if (Instance == null) Instance = this;
    }

    public void CollectItem()
    {
        currentItemAmount++;

        if (currentItemAmount >= totalItemAmount)
        {
            finishScreen.SetActive(true);
            WinParticle.Play();

            sfxParticle.pitch = Random.Range(0.9f, 1.2f);
            sfxParticle.PlayOneShot(sfxParticle.clip);

            sfxYey.pitch = Random.Range(0.9f, 1.2f);
            sfxYey.PlayOneShot(sfxYey.clip);
        }
    }
}
