using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Tap : MonoBehaviour
{
    [SerializeField] protected UnityEvent<Collider2D> onTapObject;
    [SerializeField] protected UnityEvent<Vector2> onTapPosition;
    private Camera mainCamera;
    private bool bolehDitekenAtauGa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bolehDitekenAtauGa) return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            OnTap();
        }
    }

    public void SetBolehDitekan(bool _boleh)
    {
        bolehDitekenAtauGa = _boleh;
    }

    protected void OnTap()
    {
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 posisiDiTeken = Camera.main.ScreenToWorldPoint(touchPos);

        RaycastHit2D hit = Physics2D.Raycast(posisiDiTeken, Vector2.zero);

        if(hit.collider != null)
        {
            onTapObject.Invoke(hit.collider);
        }
        else
        {
            onTapPosition.Invoke(posisiDiTeken);
        }
    }
}