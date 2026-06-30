using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NgecekSalahTeken : MonoBehaviour
{
    [SerializeField] private int intBanyakSalahYangDikasihMira = 4;
    [SerializeField] private float fltDelayXnyaIlang = 0.5f;
    [SerializeField] private GameObject objSiX;
    [SerializeField] private Vector2 offset;
    [SerializeField] private UnityEvent onPlayerSalahTekan;
    [SerializeField] private UnityEvent onKalah;

    private int intTotalSalahYangDibikinPlayer = 0;

    public int getTotalSalah => intTotalSalahYangDibikinPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlayerKetekenYangSalah(Vector2 _posisiTekenPlayer)
    {
        objSiX.SetActive(true);
        objSiX.transform.position = _posisiTekenPlayer + offset;

        intTotalSalahYangDibikinPlayer++;

        if(intTotalSalahYangDibikinPlayer >= intBanyakSalahYangDikasihMira)
        {
            onKalah.Invoke();
        }
        
        onPlayerSalahTekan.Invoke();        

        StartCoroutine(IEDelayMatiinX());
    }

    protected IEnumerator IEDelayMatiinX()
    {
        yield return new WaitForSeconds(fltDelayXnyaIlang);

        objSiX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
