using Solo.MOST_IN_ONE;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapHiddenObject : MonoBehaviour
{
    [SerializeField] private GameObject squareTerkait;
    [SerializeField] private Sprite spriteAwal;
    [SerializeField] private Sprite spriteSaatDitap;
    [SerializeField] private float alphaKotakAktif = 1f;
    [SerializeField] private float alphaKotakNonaktif = 0.3f;
    [SerializeField] private AudioClip tapSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject objBener;

    bool isFound = false;

    private Camera mainCamera;
    private SpriteRenderer spriteRendererSegitiga;
    private SpriteRenderer spriteRendererKotak;

    private GameManager gameManager;

    
    void Start()
    {
    
        mainCamera = Camera.main;
        spriteRendererSegitiga = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;

        if (squareTerkait != null)
            spriteRendererKotak = squareTerkait.GetComponent<SpriteRenderer>();

        if (spriteAwal != null)
            spriteRendererSegitiga.sprite = spriteAwal;

        if (spriteRendererKotak != null)
        {
            Color c = spriteRendererKotak.color;
            c.a = alphaKotakNonaktif;
            spriteRendererKotak.color = c;
        }
    }

    void Update()
    {
        // if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        // {
        //     Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        //     Vector2 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

        //     RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        //     if (hit.collider != null && hit.collider.gameObject == gameObject)
        //     {
                
        //     }
        // }
    }

    public void CheckTapKenaIniObject(Collider2D _collider)
    {
        if(_collider.gameObject == gameObject)
        {
            if (spriteSaatDitap != null)
                spriteRendererSegitiga.sprite = spriteSaatDitap;

            if (spriteRendererKotak != null)
            {
                Color c = spriteRendererKotak.color;
                c.a = alphaKotakAktif;
                spriteRendererKotak.color = c;
            }

            if (!isFound)
            {                    
                isFound = true;
            }
            

            if (tapSound != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(tapSound);
            }

            if(isFound)
            objBener.SetActive(true);

            MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.Success);
        }
    }

    public void CollectItem()
    {
            if (closeSound != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(closeSound);
            }

        gameManager.CollectItem();
    }
}