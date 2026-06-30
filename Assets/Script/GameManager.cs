using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected Tap tap;

    public static GameManager Instance;

    public int totalItemAmount = 5;
    public int currentItemAmount = 0;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] ParticleSystem WinParticle;
    public AudioSource sfxParticle;
    public AudioSource sfxYey;
    public AudioSource sfxWinRiweh;
    public AudioSource sfxSingSing;

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

            tap.SetBolehDitekan(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
