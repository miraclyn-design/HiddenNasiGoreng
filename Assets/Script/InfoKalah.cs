using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoKalah : MonoBehaviour
{
    [SerializeField] private Image[] arrImgSilang;
    [SerializeField] private Sprite sprKalah;
    [SerializeField] private NgecekSalahTeken cekSalah;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SalahTeken()
    {
        arrImgSilang[cekSalah.getTotalSalah-1].sprite = sprKalah;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
